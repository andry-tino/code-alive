/*
	Andrea Tino - 2018
*/

#ifndef TRIANGULATOR_H
#define TRIANGULATOR_H

#include <CGAL/Exact_predicates_inexact_constructions_kernel.h>
#include <CGAL/Triangulation_3.h>

typedef CGAL::Exact_predicates_inexact_constructions_kernel K;
typedef CGAL::Triangulation_3<K>      Triangulation;
typedef Triangulation::Cell_handle    Cell_handle;
typedef Triangulation::Vertex_handle  Vertex_handle;
typedef Triangulation::Locate_type    Locate_type;
typedef Triangulation::Point          Point;

extern "C" __declspec(dllexport)
int maintri();

#endif // TRIANGULATOR_H
