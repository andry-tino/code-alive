/*
Andrea Tino - 2018
*/

#include <iostream>
#include <list>
#include <vector>
#include <exception>
#include <set>
#include <map>

#include <boost/foreach.hpp>

#include <CGAL/Exact_predicates_inexact_constructions_kernel.h>
#include <CGAL/Triangulation_3.h>
#include <CGAL/Polygon_mesh_processing/triangulate_faces.h>
#include <CGAL/Polyhedron_3.h>
#include <CGAL/Surface_mesh.h>
#include <CGAL/convex_hull_3.h>
#include <CGAL/Simple_cartesian.h>
#include <CGAL/point_generators_3.h>
#include <CGAL/algorithm.h>

#include "DelaunayTriangulator.h"

// Constructors

CodeAlive::Triangulation::DelaunayTriangulator::DelaunayTriangulator(const int& number_vertices)
{
	this->points = std::vector<Point>();
	this->create_rnd_points(number_vertices, 1);
	this->performed = false;
}

CodeAlive::Triangulation::DelaunayTriangulator::DelaunayTriangulator(const std::list<CodeAlive::Triangulation::Point>& vertices)
{
	this->points = std::vector<Point>(vertices.begin(), vertices.end());
	this->performed = false;
}

CodeAlive::Triangulation::DelaunayTriangulator::DelaunayTriangulator(const std::vector<CodeAlive::Triangulation::Point>& vertices)
{
	this->points = std::vector<Point>(vertices.begin(), vertices.end());
	this->performed = false;
}

CodeAlive::Triangulation::DelaunayTriangulator::DelaunayTriangulator(const DelaunayTriangulator& other)
{
	this->points = std::vector<Point>(other.points.begin(), other.points.end());
	this->vertices = std::vector<Point>(other.vertices.begin(), other.vertices.end());
	this->triangles = std::vector<int>(other.triangles.begin(), other.triangles.end());
	this->performed = other.performed;
}

CodeAlive::Triangulation::DelaunayTriangulator::~DelaunayTriangulator()
{
	this->points.clear();
	this->vertices.clear();
	this->triangles.clear();
}

// Members

void CodeAlive::Triangulation::DelaunayTriangulator::perform()
{
	typedef CGAL::Polyhedron_3<K>           Polyhedron_3;
	typedef CGAL::Surface_mesh<Point_3>     Surface_mesh;
	typedef Surface_mesh::Vertex_index		vertex_descriptor;
	typedef Surface_mesh::Face_index		face_descriptor;

	// Convert into proper CGAL recognized points
	std::list<Point_3> points;
	for (std::vector<Point>::const_iterator it = this->points.begin(); it != this->points.end(); it++) {
		points.push_front(Point_3(it->X, it->Y, it->Z));
	}

	// Compute triangulation on the convex hull
	Surface_mesh sm;
	CGAL::convex_hull_3(points.begin(), points.end(), sm);
	CGAL::Polygon_mesh_processing::triangulate_faces(sm);

	// Confirm that all faces are triangles.
	BOOST_FOREACH(boost::graph_traits<Surface_mesh>::face_descriptor fit, faces(sm))
		if (next(next(halfedge(fit, sm), sm), sm) != prev(halfedge(fit, sm), sm))
			throw std::exception("Error: non-triangular face left in mesh");

	// Populate the set of vertices to get the subset of vertices that were used for the triangulation
	std::set<Point_3> vertices;
	BOOST_FOREACH(boost::graph_traits<Surface_mesh>::face_descriptor fit, faces(sm)) {
		BOOST_FOREACH(vertex_descriptor vd, vertices_around_face(sm.halfedge(fit), sm)) {
			Point_3 p = sm.point(vd);
			vertices.insert(p);
		}
	}

	// Populate the map to get the index of a vertex
	// Also populate the array of vertices in the order defined
	{ int i = 0;
		for (std::set<Point_3>::const_iterator it = vertices.begin(); it != vertices.end(); it++) {
			this->p2i[*it] = i++;

			Point v; v.X = it->x(); v.Y = it->y(); v.Z = it->z();
			this->vertices.push_back(v);
		}
	}

	// Iterate through faces and, therefore, vertices in each triangle. Populate the triangles array
	BOOST_FOREACH(boost::graph_traits<Surface_mesh>::face_descriptor fit, faces(sm)) {
		BOOST_FOREACH(vertex_descriptor vd, vertices_around_face(sm.halfedge(fit), sm)) {
			Point_3 p = sm.point(vd);
			int index = this->p2i[p];

			this->triangles.push_back(index);
		}
	}

	this->performed = true;
}

int CodeAlive::Triangulation::DelaunayTriangulator::get_vertex_index(const CodeAlive::Triangulation::Point& point)
{
	Point_3 p(point.X, point.Y, point.Z);
	return this->p2i[p];
}

void CodeAlive::Triangulation::DelaunayTriangulator::create_rnd_points(const int& num, const double& radius)
{
	typedef CGAL::Simple_cartesian<double>				R;
	typedef R::Point_3									CGALPoint;
	typedef CGAL::Creator_uniform_3<double, CGALPoint>	Creator;

	// Create test point set. Prepare a vector for 1000 points.
	std::vector<CGALPoint> points;
	points.reserve(num);

	// Create points within a sphere of specified radius.
	CGAL::Random_points_in_sphere_3<CGALPoint, Creator> g(radius);
	CGAL::cpp11::copy_n(g, num, std::back_inserter(points));

	// Use a random permutation to hide the creation history of the point set.
	CGAL::cpp98::random_shuffle(points.begin(), points.end());

	// Fill the instance vector
	this->points.clear();
	for (std::vector<CGALPoint>::iterator it = points.begin(); it != points.end(); it++) {
		Point p(it->x(), it->y(), it->z());
		this->points.push_back(p);
	}
}
