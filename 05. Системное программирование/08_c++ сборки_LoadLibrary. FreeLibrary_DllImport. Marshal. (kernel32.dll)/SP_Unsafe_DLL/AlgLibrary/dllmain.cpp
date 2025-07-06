// dllmain.cpp : Defines the entry point for the DLL application.
#include "framework.h"

extern "C" __declspec(dllexport) int Test(int a, int b)
{
    return a + b;
}

extern "C" __declspec(dllexport) void Test2()
{
    MessageBox(0, "Hello", "OK!!!", MB_OK);
}

extern "C" __declspec(dllexport)
void SelectionSort(int* a, long size) {
    long i, j, k;
    int x;

    for (i = 0; i < size; i++) {
        k = i; x = a[i];

        for (j = i + 1; j < size; j++)
            if (a[j] < x) {
                k = j; x = a[j];
            }

        a[k] = a[i]; a[i] = x;
    }
}

extern "C" __declspec(dllexport) int array_sum(unsigned int* arr, int length)
{
    int sum = 0;
    for (int i = 0; i < length; i++)
    {
        sum += arr[i];
    }
    return sum;
}

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

