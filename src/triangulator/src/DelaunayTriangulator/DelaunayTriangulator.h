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
			typedef std::vector<Point>::const_iterator vertices_const_iterator;
			typedef std::vector<int>::const_iterator triangles_const_iterator;

		private: // Internal state
			std::vector<Point> points;
			std::vector<Point> vertices; // Respects the index order
			std::vector<int> triangles; // References indices in vertices vector
			bool performed;

		public: // Ctors
			explicit DelaunayTriangulator(const std::list<Point>&);
			explicit DelaunayTriangulator(const std::vector<Point>&);
			DelaunayTriangulator(const DelaunayTriangulator&);
			~DelaunayTriangulator();

		public:
			void perform();

			vertices_const_iterator vertices_begin() const { return this->vertices.begin(); }
			vertices_const_iterator vertices_end() const { return this->vertices.end(); }
			triangles_const_iterator triangles_begin() const { return this->triangles.begin(); }
			triangles_const_iterator triangles_end() const { return this->triangles.end(); }
		};

	} // ns Triangulation
} // ns CodeAlive

#endif // DELAUNAYTRIANGULATOR_H
