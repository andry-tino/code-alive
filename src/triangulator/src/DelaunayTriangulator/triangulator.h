/*
Andrea Tino - 2018
DLL Exposure header.
*/

#ifndef TRIANGULATOR_H
#define TRIANGULATOR_H

/*
Instructs to perform a triangulation.
*/
extern "C" __declspec(dllexport)
void codealive_triangulator_perform(int);

/*
Instructs to clear the triangulation.
*/
extern "C" __declspec(dllexport)
void codealive_triangulator_dispose();

/*
Gets the number of vertices in the computed triangulation.
*/
extern "C" __declspec(dllexport)
int codealive_triangulator_get_vertices_num();

/*
Gets the length of the triangles vector.
*/
extern "C" __declspec(dllexport)
int codealive_triangulator_get_triangles_vlen();

/*
Gets the vertex at the specified index.
*/
extern "C" __declspec(dllexport)
double codealive_triangulator_get_vertex(int, int);

/*
Gets the array of triangle indices.
*/
extern "C" __declspec(dllexport)
int* codealive_triangulator_get_triangles();

#endif // TRIANGULATOR_H
