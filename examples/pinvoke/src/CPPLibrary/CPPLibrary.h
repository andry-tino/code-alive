#ifndef CPPLIB
#define CPPLIB

struct MyStruct {
	double	a;
	int		b;
	double	c;
};

extern "C" __declspec(dllexport)
int fun1(int);

extern "C" __declspec(dllexport)
int fun2(int*, int);

extern "C" __declspec(dllexport)
int* fun3();

extern "C" __declspec(dllexport)
int* fun4(int*, int);

extern "C" __declspec(dllexport)
MyStruct fun5();

extern "C" __declspec(dllexport)
int relmem(int*);

#endif
