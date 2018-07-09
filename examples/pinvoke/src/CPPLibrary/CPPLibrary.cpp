// CPPLibrary.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "CPPLibrary.h"

int fun1(int p1) {
	return p1 + 1;
}

int fun2(int* p1, int l) {
	return *(p1 + l - 1) - *(p1 + 0);
}
