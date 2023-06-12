function main()
    local a = 1
    local b = 2
    local c = a + b;  
    return c;
end


-- luac54.exe -l -l -p main.lua

0 params, 3 slots, 0 upvalues, 3 locals, 0 constants, 0 functions
1       [2]     LOADI           0 1
2       [3]     LOADI           1 2
3       [4]     ADD             2 0 1
4       [4]     MMBIN           0 1 6   ; __add
5       [5]     RETURN1         2