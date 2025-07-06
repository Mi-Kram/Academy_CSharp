#include "windows.h"
#include "sstream"
#include <io.h>
#include "direct.h"
#include "stdio.h"
#include "Words.h"
#include <algorithm>
#include <fstream>


const char* delims = " 1234567890`~!@#¹$ % ^&*()_ -= +/ .\\ | [] {}\'\";:?<>,\n\t\r";


char* NewWay(const char* way, const char* New)
{
	char* buf = new char[strlen(way) + strlen(New) + 2]{ 0 };

	strcpy(buf, way);
	strcat(buf, "\\");
	strcat(buf, New);

	return buf;
}

char* ToLower(const char* str)
{
	int len = strlen(str);
	char* result = new char[len + 1]{ 0 };
	//strcpy(result, str);

	for (int i = 0; i < len; i++)
	{
		if (isupper(str[i]) == 0)result[i] = (char)tolower(str[i]);
		else result[i] = str[i];
	}

	return result;
}

bool camparator(std::pair<char*, int>& a, std::pair<char*, int>& b)
{
	if (a.second == b.second) return strcmp(a.first, b.first) < 0;
	return a.second < b.second;
}

bool IsTXTFile(char* file)
{
	return (strlen(file) >= 4) && (_stricmp(_strrev(file), "txt.") == 0);
}

std::vector<std::pair<char*, int>> Words::GetSortedWordsDictionary(const char* path)
{
	std::map<char*, int> dic = GetWordsDictionary(path);

	std::vector<std::pair<char*, int>> result;
	for (const std::pair<char*, int>& pair : dic)
	{
		result.push_back(pair);
	}
	std::sort(result.begin(), result.end(), camparator);

	return result;
}

std::map<char*, int> Words::GetWordsDictionary(const char* path)
{
	std::map<char*, int> result;

	char* fileName = new char[strlen(path) + 5];

	strcpy(fileName, path);
	strcat(fileName, "\\*");

	_finddata_t file;

	intptr_t  hFile = _findfirst(fileName, &file);

	if (hFile == -1) return result;

	if (file.attrib & _A_SUBDIR) {
		if ((strcmp(file.name, ".") != 0) && (strcmp(file.name, "..") != 0))
		{
			char* newPath = NewWay(path, file.name);

			std::map<char*, int> dic = GetWordsDictionary(newPath);
			for (const std::pair<char*, int>& pair : dic)
			{
				if (result.find(pair.first) != result.end()) (result[pair.first])++;
				else result[pair.first] = 1;
			}

			delete[] newPath;
		}
	}
	else
	{
		int len = strlen(file.name);

		char* subbuff = new char[5]{ 0 };
		memcpy(subbuff, &file.name[len - 4], 4);

		if (len >= 4 && _stricmp(subbuff, ".txt") == 0)
		{
			char* s = new char[strlen(path) + strlen(file.name) + 2];

			strcpy(s, path);
			strcat(s, "\\");
			strcat(s, file.name);

			try
			{
				std::ifstream fin(s);
				fin.seekg(0, std::ios::end);
				int length = fin.tellg();
				fin.seekg(0, std::ios::beg);

				char* text = new char[length + 1]{ 0 };
				fin.read(text, length);

				fin.close();

				char* strTok = new char[strlen(text) + 1]{ 0 };
				strcpy(strTok, ToLower(text));
				delete[] text;

				char* token = strtok(strTok, delims);
				while (token != nullptr)
				{
					if (strlen(token) > 0)
					{
						if (result.find(token) != result.end()) (result[token])++;
						else result[token] = 1;
					}

					token = strtok(0, delims);
				}
			}
			catch (std::exception& ex) {}

			delete[] s;
		}
		delete[] subbuff;
	}

	while (_findnext(hFile, &file) == 0)
	{
		if (file.attrib & _A_SUBDIR) {
			if ((strcmp(file.name, ".") != 0) && (strcmp(file.name, "..") != 0))
			{
				char* newPath = NewWay(path, file.name);

				std::map<char*, int> dic = GetWordsDictionary(newPath);
				for (const std::pair<char*, int>& pair : dic)
				{
					if (result.find(pair.first) != result.end()) (result[pair.first])++;
					else result[pair.first] = 1;
				}

				delete[] newPath;
			}
		}
		else
		{
			int len = strlen(file.name);

			char* subbuff = new char[5]{ 0 };
			memcpy(subbuff, &file.name[len - 4], 4);

			if (len >= 4 && _stricmp(subbuff, ".txt") == 0)
			{
				char* s = new char[strlen(path) + strlen(file.name) + 2];

				strcpy(s, path);
				strcat(s, "\\");
				strcat(s, file.name);

				try
				{
					std::ifstream fin(s);
					fin.seekg(0, std::ios::end);
					int length = fin.tellg();
					fin.seekg(0, std::ios::beg);

					char* text = new char[length + 1]{ 0 };
					fin.read(text, length);

					fin.close();

					char* strTok = new char[strlen(text) + 1]{ 0 };
					strcpy(strTok, ToLower(text));
					delete[] text;

					char* token = strtok(strTok, delims);
					while (token != nullptr)
					{
						if (strlen(token) > 0)
						{
							if (result.find(token) != result.end()) (result[token])++;
							else result[token] = 1;
						}

						token = strtok(0, delims);
					}
				}
				catch (std::exception& ex) {}

				delete[] s;
			}
			delete[] subbuff;
		}
	}

	_findclose(hFile);
	delete[] fileName;

	return result;
}
