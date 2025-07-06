#include <windows.h>

extern "C" __declspec(dllexport) int Test(int a, int b)
{
  return a+b;
}

extern "C" __declspec(dllexport) void Test2()
{
  MessageBox(0,"Hello", "OK!!!", MB_OK);
}

extern "C" __declspec(dllexport) int ArrSum(int* arr, int len)
{
	int result = 0;
	for (size_t i = 0; i < len; i++)
	{
		result += arr[i];
	}
	return result;
}

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
					 )
{
    return TRUE;
}

