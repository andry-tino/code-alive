#ifndef CPPLIB
#define CPPLIB

extern "C" __declspec(dllexport)
int fun1(int);

extern "C" __declspec(dllexport)
int fun2(int*, int);

#endif
