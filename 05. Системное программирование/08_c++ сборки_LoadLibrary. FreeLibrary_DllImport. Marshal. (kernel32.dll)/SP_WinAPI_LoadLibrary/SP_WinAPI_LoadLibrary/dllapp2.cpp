#include <windows.h>

#define ID_BUTTON1 3000

char ClassName[40] = "DLLApp";
HINSTANCE hInst;
MSG msg;

LRESULT CALLBACK WndProc(HWND wnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
    static HWND hbutton;
    switch(msg)
    {
        case WM_CREATE:
        {
            hbutton = CreateWindow("button", "Load DLL", WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON,
                20, 20, 200, 20, wnd, (HMENU)ID_BUTTON1, hInst, 0);
            return 0;
        }

        case WM_COMMAND:
        {
            if ((LOWORD(wParam) == ID_BUTTON1) && (HIWORD(wParam) == BN_CLICKED))
            {
                HMODULE hDLL = NULL;

                // динамическая загрузка DLL в память
                hDLL = LoadLibrary("DLL2.dll");

                if (hDLL)
                {
                    typedef void (DLLType)();
                    DLLType* p = NULL;

                    // получить указатель на функцию внутри DLL
                    p = (DLLType*) GetProcAddress(hDLL, "Test2");
                    if (p != NULL) 
                    {
                        // вызов функции из DLL при помощи указателя на функцию
                        (*p)();
                    }
                    else MessageBox(wnd, "DLL function not found!", "ERROR!", MB_OK);

                    typedef int (AddType)(int a, int b);
                    AddType* p2 = NULL;
                    p2 = (AddType*) GetProcAddress(hDLL, "Test");
                    if (p != NULL) {

                        // вызов функции из DLL при помощи указателя на функцию
                        int r = (*p2)(3, 5); // int r = p2(3,5);
                        char s[40];
                        itoa(r, s, 10);
                        MessageBox(wnd, s, "ok", MB_OK);
                    }
                    else MessageBox(wnd, "DLL function not found!", "ERROR!", MB_OK);

                    typedef int (ArrType)(int* arr, int len);
                    ArrType* p3 = NULL;
                    p3 = (ArrType*) GetProcAddress(hDLL, "ArrSum");
                    if (p != NULL) {
                        int arr[] = {9, 8, 7, 6, 5, 4, 3};

                        // вызов функции из DLL при помощи указателя на функцию
                        int r = p3(arr, 7);
                        char s[40];
                        itoa(r, s, 10);
                        MessageBox(wnd, s, "ok", MB_OK);
                    }
                    else MessageBox(wnd, "DLL function not found!", "ERROR!", MB_OK);

                    // выгрузка DLL из памяти
                    FreeLibrary(hDLL);
                }
                else MessageBox(wnd, "Error loading DLL!", "ERROR!", MB_OK);
            }
            return 0;
        }

        case WM_DESTROY:
        {
            PostQuitMessage(0);
            return 0;
        }
    }
    return DefWindowProc(wnd,msg,wParam,lParam);
}

int APIENTRY WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nShowCmd){
    WNDCLASS wc;
    HWND hwnd;
    hInst = hInstance;

    wc.lpszClassName = ClassName;
    wc.hInstance = hInstance;
    wc.lpfnWndProc = WndProc;
    wc.style = 0;
    wc.lpszMenuName = NULL;
    wc.cbClsExtra = 0;
    wc.cbWndExtra = 0;
    wc.hCursor = LoadCursor(NULL, IDC_ARROW);
    wc.hIcon = LoadIcon(NULL, IDI_APPLICATION);
    wc.hbrBackground = (HBRUSH)(COLOR_WINDOW+1);

    int r = RegisterClass(&wc);

    hwnd = CreateWindow(ClassName, "Main window", WS_OVERLAPPEDWINDOW | WS_VISIBLE, 200,200,300,300,
        NULL, NULL, hInstance, 0);
    
    if(!hwnd) return 0;

    while(GetMessage(&msg,0,0,0))
    {
		TranslateMessage(&msg);
        DispatchMessage(&msg);
    }

    return 1;
}
