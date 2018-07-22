/*
Andrea Tino - 2018
*/

#include <iostream>
#include <list>
#include <vector>
#include <exception>

#include <boost/foreach.hpp>

#include <CGAL/Exact_predicates_inexact_constructions_kernel.h>
#include <CGAL/Triangulation_3.h>
#include <CGAL/Polygon_mesh_processing/triangulate_faces.h>
#include <CGAL/Polyhedron_3.h>
#include <CGAL/Surface_mesh.h>
#include <CGAL/convex_hull_3.h>

#include "DelaunayTriangulator.h"

// Constructors

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
	typedef CGAL::Exact_predicates_inexact_constructions_kernel  K;
	typedef CGAL::Polyhedron_3<K>           Polyhedron_3;
	typedef K::Point_3                      Point_3;
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

	// Iterate through faces and, therefore, vertices in each triangle
	BOOST_FOREACH(boost::graph_traits<Surface_mesh>::face_descriptor fit, faces(sm)) {
		BOOST_FOREACH(vertex_descriptor vd, vertices_around_face(sm.halfedge(fit), sm)) {
			Point_3 p = sm.point(vd);
			double x = sm.point(vd).x();
			double y = sm.point(vd).y();
			double z = sm.point(vd).z();

			std::cout << vd << " (" << p << "),";
		}

		std::cout << std::endl;
	}

	this->performed = true;
}
