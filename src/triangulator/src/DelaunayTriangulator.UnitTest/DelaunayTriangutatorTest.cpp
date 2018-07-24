#include "stdafx.h"
#include "CppUnitTest.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

using namespace CodeAlive::Triangulation;

namespace TriangulatorUnitTest
{
	TEST_CLASS(DelaunayTriangulatorTest)
	{

	public:
		
		TEST_METHOD(PerformTriangulation)
		{
			std::list<Point> points;
			points.push_front(Point(0, 0, 0));
			points.push_front(Point(2, 0, 0));
			points.push_front(Point(0, 2, 0));
			points.push_front(Point(2, 2, 0));
			points.push_front(Point(1, 1, 1));

			DelaunayTriangulator triangulator(points);
			triangulator.perform();
		}

		TEST_METHOD(TestTriangulation)
		{
			std::list<Point> points;
			points.push_front(Point(0, 0, 0));
			points.push_front(Point(2, 0, 0));
			points.push_front(Point(0, 2, 0));
			points.push_front(Point(2, 2, 0));
			points.push_front(Point(1, 1, 1));

			DelaunayTriangulator triangulator(points);
			triangulator.perform();

			int vertices_num = 0;
			for (DelaunayTriangulator::vertices_const_iterator it = triangulator.vertices_begin(); it != triangulator.vertices_end(); it++) {
				Point p = *it;
				vertices_num++;
			}

			Assert::AreEqual<int>(5, vertices_num);

			int triangles_num = 0;
			for (DelaunayTriangulator::triangles_const_iterator it = triangulator.triangles_begin(); it != triangulator.triangles_end(); it++) {
				int i = *it;
				triangles_num++;
			}

			Assert::AreEqual<int>(6 * 3, triangles_num);
		}

		TEST_METHOD(PerformTriangulationOverRandomSetOfPoints)
		{
			const int num = 30;
			DelaunayTriangulator triangulator(num);
			triangulator.perform();

			int vertices_num = 0;
			for (DelaunayTriangulator::vertices_const_iterator it = triangulator.vertices_begin(); it != triangulator.vertices_end(); it++) {
				Point p = *it;
				vertices_num++;
			}

			Assert::IsTrue(vertices_num > 0);
			Assert::IsTrue(vertices_num <= num);

			int triangles_num = 0;
			for (DelaunayTriangulator::triangles_const_iterator it = triangulator.triangles_begin(); it != triangulator.triangles_end(); it++) {
				int i = *it;
				triangles_num++;
			}

			Assert::IsTrue(triangles_num > 4); // A minimal quad at least
		}

	}; // class
} // ns
