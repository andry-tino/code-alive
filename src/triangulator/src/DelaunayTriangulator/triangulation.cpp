/*
Andrea Tino - 2018
DLL Exposure file.
*/

#include "DelaunayTriangulator.h"
#include "triangulator.h"

using namespace CodeAlive::Triangulation;

// Memory area
DelaunayTriangulator* triangulator;

// Calls

void codealive_triangulator_perform(int num_of_points) {
	triangulator = new DelaunayTriangulator(num_of_points);
	triangulator->perform();
}

void codealive_triangulator_dispose() {
	delete triangulator;
}

int codealive_triangulator_get_vertices_num() {
	return triangulator->vertices.size();
}

int codealive_triangulator_get_triangles_vlen() {
	return triangulator->triangles.size();
}

double codealive_triangulator_get_vertex(int index, int vindex) {
	Point p = triangulator->vertices[index];

	switch (vindex) {
	case 0: return p.X;
	case 1: return p.Y;
	case 2: return p.Z;
	}

	return p.X;
}

int* codealive_triangulator_get_triangles() {
	return &(triangulator->triangles[0]);
}
