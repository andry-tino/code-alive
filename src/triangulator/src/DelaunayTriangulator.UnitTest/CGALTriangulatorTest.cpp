#include "stdafx.h"
#include "CppUnitTest.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace TriangulatorUnitTest
{
	TEST_CLASS(CGALTriangulatorTest)
	{

	public:

		TEST_METHOD(PublicExampleTriangulation)
		{
			typedef CGAL::Exact_predicates_inexact_constructions_kernel K;
			typedef CGAL::Triangulation_3<K>			CGALTriangulation;
			typedef CGALTriangulation::Cell_handle		Cell_handle;
			typedef CGALTriangulation::Vertex_handle	Vertex_handle;
			typedef CGALTriangulation::Locate_type		Locate_type;
			typedef CGALTriangulation::Point			Point;

			// Construction from a list of points
			std::list<Point> points;
			points.push_front(Point(0, 0, 0));
			points.push_front(Point(1, 0, 0));
			points.push_front(Point(0, 1, 0));

			// Perform triangulation
			CGALTriangulation T(points.begin(), points.end());
			CGALTriangulation::size_type n = T.number_of_vertices();

			// Insert more points but using a vector instead this time
			std::vector<Point> more_points(3);
			more_points[0] = Point(0, 0, 1);
			more_points[1] = Point(1, 1, 1);
			more_points[2] = Point(2, 2, 2);
			n += T.insert(more_points.begin(), more_points.end());

			Assert::AreEqual<CGALTriangulation::size_type>(6, n);	// 6 points have been inserted
			Assert::IsTrue(T.is_valid());							// Checking validity of T

			Locate_type lt;
			int li, lj;
			Point p(0, 0, 0);
			Cell_handle c = T.locate(p, lt, li, lj);

			// p is the vertex of c of index li:
			Assert::IsTrue(lt == CGALTriangulation::VERTEX);
			Assert::IsTrue(c->vertex(li)->point() == p);

			Vertex_handle v = c->vertex((li + 1) & 3);
			// v is another vertex of c
			Cell_handle nc = c->neighbor(li);
			// nc = neighbor of c opposite to the vertex associated with p
			// nc must have vertex v
			int nli;
			Assert::IsTrue(nc->has_vertex(v, nli)); // nli is the index of v in nc

			std::ofstream oFileT("output", std::ios::out);
			// writing file output;
			oFileT << T;

			CGALTriangulation T1;
			std::ifstream iFileT("output", std::ios::in);
			// reading file output;
			iFileT >> T1;
			Assert::IsTrue(T1.is_valid());
			Assert::AreEqual<CGALTriangulation::size_type>(T.number_of_vertices(), T1.number_of_vertices());
			Assert::AreEqual<CGALTriangulation::size_type>(T.number_of_cells(), T1.number_of_cells());
		}

		TEST_METHOD(PolyhedronMesh)
		{
			typedef CGAL::Exact_predicates_inexact_constructions_kernel  K;
			typedef CGAL::Polyhedron_3<K>           Polyhedron_3;
			typedef K::Point_3                      Point_3;
			typedef CGAL::Surface_mesh<Point_3>     Surface_mesh;

			std::list<Point_3> points;
			points.push_front(Point_3(0, 0, 0));
			points.push_front(Point_3(2, 0, 0));
			points.push_front(Point_3(0, 2, 0));
			points.push_front(Point_3(2, 2, 0));
			points.push_front(Point_3(1, 1, 1));
			
			// Define polyhedron to hold convex hull
			Polyhedron_3 poly;

			std::stringstream buffer; // Buffer

			// Compute convex hull of non-collinear points
			CGAL::convex_hull_3(points.begin(), points.end(), poly);
			buffer << "The convex hull contains " << poly.size_of_vertices() << " vertices" << std::endl;

			Surface_mesh sm;
			CGAL::convex_hull_3(points.begin(), points.end(), sm);
			buffer << "The convex hull contains " << num_vertices(sm) << " vertices" << std::endl;

			Assert::AreEqual<std::string>(std::string("Some"), buffer.str());
		}

		// Use this
		TEST_METHOD(TriangulatePolygonMesh)
		{
			typedef CGAL::Exact_predicates_inexact_constructions_kernel  K;
			typedef CGAL::Polyhedron_3<K>           Polyhedron_3;
			typedef K::Point_3                      Point_3;
			typedef CGAL::Surface_mesh<Point_3>     Surface_mesh;
			typedef Surface_mesh::Vertex_index		vertex_descriptor;
			typedef Surface_mesh::Face_index		face_descriptor;

			std::set<Point_3> vertices;
			std::map<int, Point_3> map_i2p;
			std::map<Point_3, int> map_p2i;
			std::vector<int> triangles;

			std::list<Point_3> points;
			points.push_front(Point_3(0, 0, 0));
			points.push_front(Point_3(2, 0, 0));
			points.push_front(Point_3(0, 2, 0));
			points.push_front(Point_3(2, 2, 0));
			points.push_front(Point_3(1, 1, 1));

			std::stringstream buffer; // Buffer

			Surface_mesh sm;
			CGAL::convex_hull_3(points.begin(), points.end(), sm);
			//buffer << "The convex hull contains " << num_vertices(sm) << " vertices" << std::endl;

			CGAL::Polygon_mesh_processing::triangulate_faces(sm);

			// Confirm that all faces are triangles.
			BOOST_FOREACH(boost::graph_traits<Surface_mesh>::face_descriptor fit, faces(sm))
				if (next(next(halfedge(fit, sm), sm), sm) != prev(halfedge(fit, sm), sm))
					Assert::Fail(L"Error: non-triangular face left in mesh", LINE_INFO());

			// Get vertices
			BOOST_FOREACH(boost::graph_traits<Surface_mesh>::face_descriptor fit, faces(sm)) {
				BOOST_FOREACH(vertex_descriptor vd, vertices_around_face(sm.halfedge(fit), sm)) {
					Point_3 p = sm.point(vd);
					vertices.insert(p);
				}
			}

			buffer << "Set size: " << vertices.size() << std::endl << std::endl;

			// Populate map
			int i = 0;
			buffer << std::endl << std::endl << "vertices: ";
			for (std::set<Point_3>::const_iterator it = vertices.begin(); it != vertices.end(); it++) {
				map_i2p[i] = *it;
				map_p2i[*it] = i;

				buffer << (*it) << " = " << i << ", ";

				i++;
			}
			buffer << std::endl << std::endl;
			buffer << "Map1 size: " << map_i2p.size() << std::endl; // don't need this actually
			buffer << "Map2 size: " << map_p2i.size() << std::endl << std::endl;

			BOOST_FOREACH(boost::graph_traits<Surface_mesh>::face_descriptor fit, faces(sm)) {
				CGAL::SM_Halfedge_index he1 = halfedge(fit, sm);
				CGAL::SM_Halfedge_index he2 = next(he1, sm);
				CGAL::SM_Halfedge_index he3 = next(he2, sm);

				BOOST_FOREACH(vertex_descriptor vd, vertices_around_face(sm.halfedge(fit), sm)) {
					Point_3 p = sm.point(vd);
					double x = sm.point(vd).x();
					double y = sm.point(vd).y();
					double z = sm.point(vd).z();

					int i = map_p2i[p];
					triangles.push_back(i);

					//buffer << vd << " (" << x << " " << y << " " << z << "),";
					buffer << vd << " (" << p << "),";
				}

				buffer << std::endl;
			}

			buffer << std::endl << "triangles: ";
			for (std::vector<int>::const_iterator it = triangles.begin(); it != triangles.end(); it++) {
				buffer << (*it) << " ";
			}

			Assert::AreEqual<std::string>(std::string("Some"), buffer.str());
		}

		TEST_METHOD(GenerateRandomPoints)
		{
			typedef CGAL::Simple_cartesian<double>				R;
			typedef R::Point_3									Point;
			typedef CGAL::Creator_uniform_3<double, Point>		Creator;
			typedef std::vector<Point>							Vector;

			// Create test point set. Prepare a vector for 1000 points.
			Vector points;
			points.reserve(900);

			// Create 600 points within a sphere of radius 150.
			CGAL::Random_points_in_sphere_3<Point, Creator> g(150.0);
			CGAL::cpp11::copy_n(g, 600, std::back_inserter(points));

			// Create 200 points from a 15 x 15 grid.
			CGAL::points_on_cube_grid_3(250.0, 200, std::back_inserter(points), Creator());

			// Select 100 points randomly and append them at the end of
			// the current vector of points.
			CGAL::random_selection(points.begin(), points.end(), 100, std::back_inserter(points));
			
			// Check that we have really created 1000 points.
			Assert::AreEqual<size_t>(900, points.size());
			// Use a random permutation to hide the creation history
			// of the point set.
			CGAL::cpp98::random_shuffle(points.begin(), points.end());

			// Check range of values.
			std::stringstream buffer; // Buffer
			for (Vector::iterator i = points.begin(); i != points.end(); i++) {
				Assert::IsTrue(i->x() <= 251);
				Assert::IsTrue(i->x() >= -251);
				Assert::IsTrue(i->y() <= 251);
				Assert::IsTrue(i->y() >= -251);

				buffer << (*i) << std::endl;
			}

			Assert::AreEqual<std::string>(std::string("Some"), buffer.str());
		}

		TEST_METHOD(TriangulationOnPyramid)
		{
			typedef CGAL::Exact_predicates_inexact_constructions_kernel K;
			typedef CGAL::Delaunay_triangulation_3<K>	CGALTriangulation;
			typedef CGALTriangulation::Cell_handle		Cell_handle;
			typedef CGALTriangulation::Vertex_handle	Vertex_handle;
			typedef CGALTriangulation::Locate_type		Locate_type;
			typedef CGALTriangulation::Point			Point;

			// Construction from a list of points
			std::list<Point> points;
			points.push_front(Point(0, 0, 0));
			points.push_front(Point(2, 0, 0));
			points.push_front(Point(0, 2, 0));
			points.push_front(Point(2, 2, 0));
			points.push_front(Point(1, 1, 1));

			// Perform triangulation
			CGALTriangulation T(points.begin(), points.end());
			CGALTriangulation::size_type n = T.number_of_vertices();
			CGALTriangulation::size_type ne = T.number_of_finite_edges();
			CGALTriangulation::size_type nf = T.number_of_finite_facets();

			std::stringstream buffer; // Buffer
			
			std::map<Point, int> vertex_ids;
			for (CGALTriangulation::Finite_vertices_iterator it = T.finite_vertices_begin(); 
				it != T.finite_vertices_end(); 
				it++)
			{
				CGALTriangulation::Triangulation_data_structure::Vertex v = *it;

				// Invoking operator[], causes size to grow first, the assignment is performed. Therefore we need -1
				vertex_ids[v.point()] = vertex_ids.size() - 1;
			}

			for (CGALTriangulation::Finite_edges_iterator it = T.finite_edges_begin();
				it != T.finite_edges_end();
				it++) 
			{
				CGALTriangulation::Triangulation_data_structure::Edge e = *it;
				CGALTriangulation::Triangulation_data_structure::Cell_handle c = e.first;
				int i = e.second;
				int j = e.third;

				CGALTriangulation::Triangulation_data_structure::Vertex_handle a = c->vertex(i);
				Point pa = a->point();
				Point pb = c->vertex(j)->point();
				
				int ida = (vertex_ids.find(pa) == vertex_ids.end()) ? 0 : vertex_ids[pa];
				int idb = (vertex_ids.find(pb) == vertex_ids.end()) ? 0 : vertex_ids[pb];

				buffer << "Edge: " << pa << " (" << ida << ") to " << pb << " (" << idb << ")" << std::endl;
			}

			//Assert::AreEqual<std::string>(std::string("Some"), buffer.str());

			std::stringstream buffer2; // Buffer
			for (CGALTriangulation::Finite_facets_iterator it = T.finite_facets_begin();
				it != T.finite_facets_end();
				it++)
			{
				std::pair<CGALTriangulation::Cell_handle, int> facet = *it;
				CGALTriangulation::Vertex_handle v1 = facet.first->vertex((facet.second + 1) % 3);
				CGALTriangulation::Vertex_handle v2 = facet.first->vertex((facet.second + 2) % 3);
				CGALTriangulation::Vertex_handle v3 = facet.first->vertex((facet.second + 3) % 3);

				buffer2 << "Facet: " << v1->point() << "," << v2->point() << ", " << v3->point() << std::endl;
			}

			Assert::AreEqual<std::string>(std::string("Some"), buffer2.str());

			Assert::AreEqual<CGALTriangulation::size_type>(5, n);
			Assert::IsTrue(T.is_valid());
		}

	}; // class
} // ns
