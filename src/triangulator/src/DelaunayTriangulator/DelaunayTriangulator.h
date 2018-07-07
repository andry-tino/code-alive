/*
Andrea Tino - 2018
*/

#ifndef DELAUNAYTRIANGULATOR_H
#define DELAUNAYTRIANGULATOR_H

#include "interop.h"

namespace CodeAlive
{
	namespace Triangulation
	{

		/*
		Describes a Delaunay triangulation in 3D.
		*/
		class EXPORT_API DelaunayTriangulator {
		private:

		public:
			DelaunayTriangulator();
			DelaunayTriangulator(const DelaunayTriangulator&);
			~DelaunayTriangulator();

		public:
			int Perform();
		};

	} // ns Triangulation
} // ns CodeAlive

#endif // DELAUNAYTRIANGULATOR_H
