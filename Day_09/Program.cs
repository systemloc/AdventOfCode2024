using System.Diagnostics;

namespace Day_09;

internal class Program {
    static void Main(string[] args) {

        char[] inputArr = Input.RealInput.Trim().ToCharArray();
        //char[] reversedInputArr = new char[inputArr.Length];
        //inputArr.CopyTo(reversedInputArr,0);
        //reversedInputArr.Reverse();

        int fileID = 0;
        ulong total = 0;
        bool isFile = true;
        int diskIndex = 0;
        int inputFromEndIndex = inputArr.Length - 1;
        for (int inputIdx = 0; inputIdx < inputArr.Length ; inputIdx++) {
            int input = inputArr[inputIdx] - 48;
            fileID = inputIdx / 2;
            if (isFile) {
                for (int offset = 0; offset < input; offset++) {
                    total += (ulong)fileID * (ulong)diskIndex;
                    diskIndex++;
                }
                isFile = false;
            } else {
                int emptyBlockLength = inputArr[inputIdx] - 48;

                Start:
                fileID = inputFromEndIndex / 2;
                int fileLength = inputArr[inputFromEndIndex] - 48;

                if (emptyBlockLength == fileLength) {
                    for (int offset = 0; offset < fileLength; offset++) {
                        total += (ulong)fileID * (ulong)diskIndex;
                        diskIndex++;
                    }
                    inputArr[inputFromEndIndex] = '0';
                    inputArr[inputFromEndIndex-1] = '0';
                    inputFromEndIndex -= 2;
                } else if (emptyBlockLength < fileLength) {
                    for (int offset = 0; offset < emptyBlockLength; offset++) {
                        total += (ulong)fileID * (ulong)diskIndex;
                        diskIndex++;
                    }
                    inputArr[inputFromEndIndex] = (fileLength - emptyBlockLength)
                                                  .ToString().ToCharArray()[0];
                } else if (emptyBlockLength > fileLength) {
                    for (int offset = 0; offset < fileLength; offset++) {
                        total += (ulong)fileID * (ulong)diskIndex;
                        diskIndex++;
                    }

                    emptyBlockLength -= fileLength;
                    inputArr[inputFromEndIndex] = '0';
                    inputArr[inputFromEndIndex-1] = '0';
                    inputFromEndIndex -= 2;
                    goto Start;
                }
                isFile = true;
            }
        }

        Console.WriteLine(total.ToString());
        return;
        // Part 1
        // I think this actually has n! complexity!

        List<int> fs = new List<int>();
        bool isEven = true;
        fileID = 0;

        foreach (char i in Input.RealInput.Trim().AsEnumerable()) {
            if (isEven) {
                FSAddFile(i, fileID);
                fileID++;
                isEven = false;
            } else {
                FSAddFree(i);
                isEven = true;
            }
        }

        int lastUsedBlock = FSLastUsedBlock();
        int firstFreeBlock = FSFirstFreeBlock();
        while (lastUsedBlock > firstFreeBlock) {
            fs[firstFreeBlock] = fs[lastUsedBlock];
            fs[lastUsedBlock] = -1;
            lastUsedBlock = FSLastUsedBlock();
            firstFreeBlock = FSFirstFreeBlock();
        }

        total = 0;
        for (int i = 0; i < fs.Count; i++) {
            if (fs[i] == -1)
                continue;
            else
                total += (ulong)i * (ulong)fs[i];
        }

        Console.WriteLine("Day 9 Part 1: " + total);

        return;
        // Part 2


        fileID = 0;
        isEven = true;
        fs = new List<int>();
        List<(int index, int length)> freeList = new();
        List<(int index, int length, int fileID)> fileList = new();
        foreach (char i in Input.RealInput.Trim().AsEnumerable()) {
            if (isEven) {
                fileList.Add((fs.Count,Convert.ToInt32(i.ToString()), fileID));
                FSAddFile(i, fileID);
                fileID++;
                isEven = false;
            } else {
                freeList.Add((fs.Count,Convert.ToInt32(i.ToString())));
                FSAddFree(i);
                isEven = true;
            }
        }

        List<(int index, int length, int fileID)> newFileList = new();
        fileList.Reverse();
        foreach ( (int index, int length, int id) in fileList ) {
            int firstFree = freeList.FindIndex(p => p.length >= length && p.index < index);
            if (firstFree == -1) {
                newFileList.Add((index, length, id));
            } else {
                newFileList.Add((freeList[firstFree].index, length, id));
                if (freeList[firstFree].length == length) {
                    freeList.RemoveAt(firstFree);
                } else {
                    freeList[firstFree] = (freeList[firstFree].index + length,
                                            freeList[firstFree].length - length);
                }
            }
        }


        total = 0;
        foreach (var i in newFileList) {
            for (int j = 0; j < i.length; j++) {
                total +=
                (ulong)i.fileID * ((ulong)i.index + (ulong)j);
            }
        }
        Console.WriteLine(total.ToString());



        string FSToString() {
            List<char> output = new List<char>();
            foreach (int i in fs) {
                if (i == -1)
                    output.Add('.');
                else
                    output.AddRange(i.ToString().ToList());
            }
            return new String(output.ToArray());
        }

        int FSLastUsedBlock() {
            int output = fs.FindLastIndex(p => ! p.Equals(-1));
            return output;
        }

        int FSFirstFreeBlock() =>
            fs.FindIndex(p => p.Equals(-1));

        void FSAddFile(char input, int fileID) {
            int size = Convert.ToInt32(input.ToString());
            for (int i = 0; i < size; i++) {
                fs.Add(fileID);
            }
        }

        void FSAddFree(char input) {
            int size = Convert.ToInt32 (input.ToString());
            for (int i = 0; i < size; i++) {
                fs.Add(-1);
            }


        }
    }
}
