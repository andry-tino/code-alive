/*
Andrea Tino - 2018
*/

#ifndef DELAUNAYTRIANGULATOR_H
#define DELAUNAYTRIANGULATOR_H

#include "interop.h"

#include <vector>
#include <list>

namespace CodeAlive
{
	namespace Triangulation
	{
		/*
		Describes a point in the Euclidean space.
		*/
		struct EXPORT_API Point {
		public: // Types
			/*
			The underlying field to use: real numbers.
			*/
			typedef double RF;

		public: // Members
			RF X;
			RF Z;
			RF Y;
		};

		/*
		Describes a Delaunay triangulation in 3D of a set of points.
		This class ensures that the final set of triangles defines a convex hull properly triangulated.
		*/
		class EXPORT_API DelaunayTriangulator {
		public: // Types
			/*
			The array of vertices. The order is important, each vertex is enumerated by
			its position (0-based) in the array.
			*/
			typedef Point* VertexArray;
			/*
			The array of triangles.
			The dimension of this must always be multiple of 3. It contains the indices of points
			inside the VertexArray which form the triangles of the mesh.
			*/
			typedef int* TrianglesArray;

		private: // Internal state
			VertexArray vertices;
			TrianglesArray triangles;

		public:
			explicit DelaunayTriangulator(const std::list<Point>&);
			explicit DelaunayTriangulator(const std::vector<Point>&);
			DelaunayTriangulator(const DelaunayTriangulator&);
			~DelaunayTriangulator();

		public:
			int Perform();
		};

	} // ns Triangulation
} // ns CodeAlive

#endif // DELAUNAYTRIANGULATOR_H
