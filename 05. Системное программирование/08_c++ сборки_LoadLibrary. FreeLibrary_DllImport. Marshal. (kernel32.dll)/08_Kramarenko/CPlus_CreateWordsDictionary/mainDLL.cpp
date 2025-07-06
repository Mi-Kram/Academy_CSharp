#include "framework.h"
#include "Words.h"
#include <fstream>
#include <vector>
#include <iomanip>
#include <stdio.h>
#include <stdlib.h>


extern "C" __declspec(dllexport) void CreateWordsDictionary(char* folderPath, char* resultPath)
{
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);
    setlocale(LC_ALL, "Rus");

    std::vector<std::pair<char*, int>> dic = Words::GetSortedWordsDictionary(folderPath);

    std::ofstream fout(resultPath);
    char* tmp = new char[21];

    int w = 1;
    if (dic.size() > 0) w = strlen(_itoa(dic.back().second, tmp, 20));
    delete[] tmp;

    fout << std::right;
    fout << std::setfill('0');
    for (const std::pair<char*, int>& pair : dic)
    {
        fout << std::setw(w) << pair.second << ": " << pair.first << std::endl;
    }

    fout.close();
}



BOOL APIENTRY DllMain(HMODULE hModule,
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

