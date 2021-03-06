/*
Andrea Tino - 2018
*/

#ifndef DELAUNAYTRIANGULATOR_H
#define DELAUNAYTRIANGULATOR_H

#include "interop.h"
#include "triangulator.h"

#include <vector>
#include <list>
#include <map>

#include <CGAL/Exact_predicates_inexact_constructions_kernel.h>

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

		public: // Ctors
			Point() {
				this->X = 0;
				this->Y = 0;
				this->Z = 0;
			}

			Point(const RF& x, const RF& y, const RF& z) {
				this->X = x;
				this->Y = y;
				this->Z = z;
			}

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
			// Friendship decl
			friend int ::codealive_triangulator_get_vertices_num();
			friend int ::codealive_triangulator_get_triangles_vlen();
			friend double ::codealive_triangulator_get_vertex(int, int);
			friend int* ::codealive_triangulator_get_triangles();

		public: // Types
			typedef std::vector<Point>::const_iterator vertices_const_iterator;
			typedef std::vector<int>::const_iterator triangles_const_iterator;

		private: // Private types
			typedef CGAL::Exact_predicates_inexact_constructions_kernel K;
			typedef K::Point_3 Point_3;

		private: // Internal state
			std::vector<Point> points;
			std::vector<Point> vertices; // Respects the index order
			std::vector<int> triangles; // References indices in vertices vector

			// Helpers
			std::map<Point_3, int> p2i;
			bool performed;

		public: // Ctors
			explicit DelaunayTriangulator(const int&);
			explicit DelaunayTriangulator(const std::list<Point>&);
			explicit DelaunayTriangulator(const std::vector<Point>&);
			DelaunayTriangulator(const DelaunayTriangulator&);
			~DelaunayTriangulator();

		public:
			void perform();
			int get_vertex_index(const Point&);

			vertices_const_iterator vertices_begin() const { return this->vertices.begin(); }
			vertices_const_iterator vertices_end() const { return this->vertices.end(); }
			triangles_const_iterator triangles_begin() const { return this->triangles.begin(); }
			triangles_const_iterator triangles_end() const { return this->triangles.end(); }

		private:
			void create_rnd_points(const int&, const double&);
		};

	} // ns Triangulation
} // ns CodeAlive

#endif // DELAUNAYTRIANGULATOR_H
