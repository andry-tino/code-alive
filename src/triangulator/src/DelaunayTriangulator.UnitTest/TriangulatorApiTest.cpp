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

			int vertices_num = codealive_triangulator_get_vertices_num();
			Assert::IsTrue(vertices_num > 0);
			Assert::IsTrue(vertices_num <= num);

			int triangles_len = codealive_triangulator_get_triangles_vlen();
			Assert::IsTrue(triangles_len > 4 * 3); // At least a quad

			// Destroy triangulation
			codealive_triangulator_dispose();
		}

	}; // class
} // ns
