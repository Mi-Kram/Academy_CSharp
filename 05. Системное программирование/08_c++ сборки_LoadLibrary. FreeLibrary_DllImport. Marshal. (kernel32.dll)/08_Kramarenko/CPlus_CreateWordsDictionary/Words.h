#pragma once

#include <iostream>
#include <map>
#include <vector>
#include <windows.h>

class Words
{
public:
	static std::vector<std::pair<char*, int>> GetSortedWordsDictionary(const char* path);

private:
	static std::map<char*, int> GetWordsDictionary(const char* path);
};

