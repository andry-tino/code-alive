#include "stdafx.h"
#include "CppUnitTest.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace TriangulatorUnitTest
{
	TEST_CLASS(TriangulatorApiTest)
	{

	public:

		TEST_METHOD(PerformTriangulationAndGetInfo)
		{
			// Perform triangulation
			const int num = 30;
			codealive_triangulator_perform(num);

			// Verify vertices
			int vertices_num = codealive_triangulator_get_vertices_num();
			Assert::IsTrue(vertices_num > 0);
			Assert::IsTrue(vertices_num <= num);

			// Verify triangles
			int triangles_len = codealive_triangulator_get_triangles_vlen();
			Assert::IsTrue(triangles_len > 4 * 3); // At least a quad
			Assert::IsTrue(triangles_len % 3 == 0); // Must be a multiple of 3

			// Get triangles
			int* triangles_ptr = codealive_triangulator_get_triangles();
			Assert::IsFalse(triangles_ptr == 0);

			for (int i = 0; i < triangles_len; i++) {
				Assert::IsFalse(*(triangles_ptr + i) < 0);
				Assert::IsFalse(*(triangles_ptr + i) >= triangles_len); // Indices, so they should not overflow
			}

			// Get vertices
			double v1x = codealive_triangulator_get_vertex(0, 0);
			double v1y = codealive_triangulator_get_vertex(0, 1);
			double v1z = codealive_triangulator_get_vertex(0, 2);

			double vnx = codealive_triangulator_get_vertex(vertices_num - 1, 0);
			double vny = codealive_triangulator_get_vertex(vertices_num - 1, 1);
			double vnz = codealive_triangulator_get_vertex(vertices_num - 1, 2);

			// Destroy triangulation
			codealive_triangulator_dispose();
		}

	}; // class
} // ns
