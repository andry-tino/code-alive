#include "stdafx.h"
#include "CppUnitTest.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

typedef CGAL::Exact_predicates_inexact_constructions_kernel K;
typedef CGAL::Triangulation_3<K>			CGALTriangulation;
typedef CGALTriangulation::Cell_handle		Cell_handle;
typedef CGALTriangulation::Vertex_handle	Vertex_handle;
typedef CGALTriangulation::Locate_type		Locate_type;
typedef CGALTriangulation::Point			Point;

namespace TriangulatorUnitTest
{
	TEST_CLASS(CGALTriangulatorTest)
	{

	public:

		TEST_METHOD(CGALExampleTriangulation)
		{
			// Construction from a list of points :
			std::list<Point> L;
			L.push_front(Point(0, 0, 0));
			L.push_front(Point(1, 0, 0));
			L.push_front(Point(0, 1, 0));

			CGALTriangulation T(L.begin(), L.end());

			CGALTriangulation::size_type n = T.number_of_vertices();

			// Insertion from a vector
			std::vector<Point> V(3);
			V[0] = Point(0, 0, 1);
			V[1] = Point(1, 1, 1);
			V[2] = Point(2, 2, 2);

			n = n + T.insert(V.begin(), V.end());

			Assert::AreEqual<size_t>(6, n);	// 6 points have been inserted
			Assert::IsTrue(T.is_valid());	// Checking validity of T

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
			// nc must have vertex v :
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
			Assert::AreEqual<size_t>(T.number_of_vertices(), T1.number_of_vertices());
			Assert::AreEqual<size_t>(T.number_of_cells(), T1.number_of_cells());
		}

	}; // class
} // ns
