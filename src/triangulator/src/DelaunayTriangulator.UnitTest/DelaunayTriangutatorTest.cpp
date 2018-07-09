#include "stdafx.h"
#include "CppUnitTest.h"

#include "../DelaunayTriangulator/DelaunayTriangulator.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

using namespace CodeAlive::Triangulation;

namespace TriangulatorUnitTest
{
	TEST_CLASS(DelaunayTriangulatorTest)
	{

	public:
		
		TEST_METHOD(PerformTriangulation)
		{
			DelaunayTriangulator* triangulator = new DelaunayTriangulator();
			int result = triangulator->Perform();

			Assert::AreEqual<int>(0, result, L"Wrong result", LINE_INFO());

			delete triangulator;
		}

	}; // class
} // ns
