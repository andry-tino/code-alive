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

int* fun3() {
	int* a = new int[4] { 3, 0, 1, 2 }; // First is length
	return a;
}

int* fun4(int* p1, int l) {
	std::list<int>* lst = new std::list<int>();
	lst->push_front(10);
	lst->push_front(9);
	lst->push_front(8);
	lst->push_front(4); // The length

	std::vector<int>* v = new std::vector<int>(
		lst->begin(), lst->end());
	
	int* arr = &((*v)[0]);

	return 0;
}

int relmem(int* arr) {
	delete[] arr;
	return 0;
}
