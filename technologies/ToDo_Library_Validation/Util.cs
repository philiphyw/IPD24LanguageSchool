using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Windows;
using System.Globalization;

namespace ToDoMVVM.Library
{
    public class InvalidParameterException : Exception
    {
        public InvalidParameterException(string msg) : base(msg)
        {
        }
    }

    public class Log
    {
        static string logFileName = @"../../log.txt";
        public delegate void LogFailedSetterDelegate(string reason);
        static void logNothing(string reason) { }
        static void logOnConsole(string reason)
        {
            Console.WriteLine(reason);
            //Debug.WriteLine(reason);
        }
        static void logOnMessageBox(string reason)
        {
            //MessageBox.Show(reason, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        static void logOnFile(string reason)
        {
            try
            {
                if (File.Exists(logFileName))
                {
                    using (StreamWriter sw = File.AppendText(logFileName))
                    {
                        sw.WriteLine(reason);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.CreateText(logFileName))
                    {
                        sw.WriteLine(reason);
                    }
                }
            }
            catch (Exception ex)
                when ((ex is IOException) ||
                      (ex is UnauthorizedAccessException) || (ex is SecurityException) ||
                      (ex is ArgumentException) || (ex is PathTooLongException) ||
                      (ex is DirectoryNotFoundException) || (ex is NotSupportedException))
            {
                Debug.WriteLine(ex.Message);
            }
        }

        static bool setLogFilePathName(string fileName)
        {
            if (!String.IsNullOrEmpty(fileName) && !String.IsNullOrWhiteSpace(fileName))
            {
                logFileName = fileName;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static LogFailedSetterDelegate WriteLine = logNothing;

        public static void setLogOnConsole()
        {
            WriteLine = logOnConsole;
        }
        public static void setLogOnMessageBox()
        {
            WriteLine = logOnMessageBox;
        }
        public static void setLogOnFile()
        {
            WriteLine = logOnFile;
        }
        public static void setLogOnFile(string fileName)
        {
            if (!setLogFilePathName(fileName))
            {
                Debug.WriteLine("file name cannot be empty or blank");
            }
            WriteLine = logOnFile;
        }
        public static void setNoLog()
        {
            WriteLine = logNothing;
        }
    }

    public class FileParser<T>
    {

        public delegate string ToStringMethod(T t);
        public delegate T parseStringMethod(string s);

        public static string[] loadLinesFromFile(string filePath)
        {
            string[] allLines = default(string[]);
            try
            {
                if (File.Exists(filePath))
                {
                    using (StreamReader sr = File.OpenText(filePath))
                    {
                        if (File.Exists(filePath))
                        {
                            allLines = File.ReadAllLines(filePath);
                        }
                    }
                }
                else
                {
                    Log.WriteLine("File does not exist");
                }
            }
            catch (Exception ex)
                when ((ex is IOException) || (ex is InvalidParameterException) ||
                      (ex is UnauthorizedAccessException) || (ex is SecurityException) ||
                      (ex is ArgumentException) || (ex is PathTooLongException) ||
                      (ex is DirectoryNotFoundException) || (ex is NotSupportedException))
            {
                Log.WriteLine(ex.Message);
            }
            return allLines;
        }
        public static void saveLinesToFile(string filePath, string[] lines)
        {
            try
            {
                File.WriteAllLines(filePath, lines);
            }
            catch (Exception ex)
                when ((ex is AmbiguousMatchException) || (ex is ArgumentNullException) ||
                      (ex is IOException) ||
                      (ex is UnauthorizedAccessException) || (ex is SecurityException) ||
                      (ex is ArgumentException) || (ex is PathTooLongException) ||
                      (ex is DirectoryNotFoundException) || (ex is NotSupportedException))
            {
                Log.WriteLine(ex.Message);
            }
        }

        public static List<T> loadFromFile(string filePath, parseStringMethod parseFromString)
        {
            List<T> records = new List<T>();
            int count = 0;
            string[] allLines = loadLinesFromFile(filePath);
            foreach (var line in allLines)
            {
                count++;
                if (!String.IsNullOrEmpty(line.Trim())) // ignore empty or blank line
                {
                    try
                    {
                        T t = parseFromString(line);
                        if (!t.Equals(default(T)))
                        {
                            records.Add(t);
                        }
                        else
                        {
                            Log.WriteLine($"Error @line#{count}: string {line} cannot be parsed");
                        }
                    }
                    catch (Exception ex)
                        when ((ex is InvalidParameterException) || (ex is FormatException))
                    {
                        Log.WriteLine($"Error @line#{count}: " + ex.Message);
                    }

                }
            }
            return records;
        }
        public static void saveToFile(string filePath, List<T> records, ToStringMethod parseToString)
        {
            int count = 0;
            string[] dataLines = new string[records.Count];
            foreach (var t in records)
            {
                string line = parseToString(t);
                if (!line.Equals(default(string)))
                {
                    dataLines[count++] = line;
                }
                else
                {
                    Log.WriteLine($"instance {t.GetHashCode()} can not be parsed");
                }
            }
            saveLinesToFile(filePath, dataLines);
        }

        public static List<T> loadFromFile(string filePath)
        {
            List<T> records = new List<T>();
            try
            {
                Type classT = typeof(T);
                Type[] paramTypes = new Type[1];
                paramTypes[0] = typeof(string);
                ConstructorInfo constructorT = classT.GetConstructor(paramTypes);
                parseStringMethod method = (s) => (T)constructorT.Invoke(new object[] { s });
                records = loadFromFile(filePath, method);
            }
            catch (Exception ex)
                when ((ex is ArgumentException) || (ex is ArgumentNullException) ||
                      (ex is MemberAccessException) || (ex is MethodAccessException) ||
                      (ex is ArgumentException) || (ex is TargetParameterCountException) ||
                      (ex is NotSupportedException) || (ex is SecurityException) ||
                      (ex is TargetInvocationException))
            //TargetInvocationException: the invoked constructor throws an exception
            {
                Log.WriteLine(ex.Message);
            }
            return records;
        }
        public static void saveToFile(string filePath, List<T> records, string toStringMethodName)
        {
            try
            {
                Type classT = typeof(T);
                MethodInfo toStringMethod = classT.GetMethod(toStringMethodName);
                ToStringMethod method = (t) => (string)toStringMethod.Invoke(t, new object[] { });
                saveToFile(filePath, records, method);
            }
            catch (Exception ex)
                when ((ex is AmbiguousMatchException) || (ex is ArgumentNullException))
            {
                Log.WriteLine(ex.Message);
            }
        }

        public static bool parseString(string line, char splitter, string[] properties, out T outputInstance)
        {
            bool bSucceedSetting = false;
            outputInstance = default(T);
            try
            {
                string[] inputs = line.Split(splitter);
                if (inputs.Length != properties.Length)
                {
                    string msg = "formatted content mismatches setters";
                    throw new InvalidParameterException(msg);
                }

                Type classT = typeof(T);
                ConstructorInfo constructorT = classT.GetConstructor(Type.EmptyTypes);
                outputInstance = (T)constructorT.Invoke(new object[] { });
                MethodInfo setMethod;
                Type propertyType;

                for (int i = 0; i < properties.Length; i++)
                {
                    propertyType = classT.GetProperty(properties[i]).PropertyType;
                    setMethod = classT.GetProperty(properties[i]).GetSetMethod();
                    if (propertyType == typeof(string))
                    {
                        setMethod.Invoke(outputInstance, new object[] { inputs[i] });
                    }
                    else
                    {
                        setMethod.Invoke(outputInstance, new object[] { TypeDescriptor.GetConverter(propertyType).ConvertFromString(inputs[i]) });
                    }
                    //Debug.WriteLine(outputInstance);
                    bSucceedSetting = true;
                }
            }
            catch (Exception ex)
                when ((ex is AmbiguousMatchException) ||
                      (ex is ArgumentException) || (ex is ArgumentNullException) ||
                      (ex is MemberAccessException) || (ex is MethodAccessException) ||
                      (ex is ArgumentException) || (ex is TargetParameterCountException) ||
                      (ex is NotSupportedException) || (ex is SecurityException) ||
                      (ex is TargetInvocationException))
            {
                Log.WriteLine(ex.Message);
            }
            return bSucceedSetting;
        }
        public static List<T> loadFromFile(string filePath, char splitter, string[] properties)
        {
            T t = default(T);
            parseStringMethod method = (s) => parseString(s, splitter, properties, out t) ? t : default(T);
            return loadFromFile(filePath, method);
        }
        public static bool parseObject(T t, char splitter, string[] properties, out string outputString)
        {
            bool bSucceedSetting = false;
            outputString = default(string);
            Type classT = typeof(T);
            try
            {
                string line = "";
                for (int i = 0; i < properties.Length; i++)
                {
                    Type propertyType = classT.GetProperty(properties[i]).PropertyType;
                    MethodInfo getMethod = classT.GetProperty(properties[i]).GetGetMethod();
                    string s = getMethod.Invoke(t, new object[] { }).ToString();
                    if (i == 0)
                    {
                        line += s;
                    }
                    else
                    {
                        line += $"{splitter}{s}";
                    }
                }
                outputString = line;
                bSucceedSetting = true;
            }
            catch (Exception ex)
                when ((ex is IOException) ||
                      (ex is UnauthorizedAccessException) || (ex is SecurityException) ||
                      (ex is ArgumentException) || (ex is PathTooLongException) ||
                      (ex is DirectoryNotFoundException) || (ex is NotSupportedException) ||
                      (ex is AmbiguousMatchException) || (ex is ArgumentNullException))
            {
                Log.WriteLine(ex.Message);
            }
            return bSucceedSetting;
        }
        public static void saveToFile(string filePath, List<T> records, char splitter, string[] properties)
        {
            string line = default(string);
            ToStringMethod method = (objT) => parseObject(objT, splitter, properties, out line) ? line : default(string);
            saveToFile(filePath, records, method);
        }

        // line format: TypeInfo[splitter]DataInfo
        // T classes: children classes shall "override" the "virtual" method of parent class
        public static List<T> loadFromFileWithType(string filePath, char splitter, Type[] classTypes)
        {
            string[] dataLines = loadLinesFromFile(filePath);
            List<T> records = new List<T>();
            int count = 0;
            foreach (var line in dataLines)
            {
                count++;
                if (!String.IsNullOrEmpty(line.Trim())) // ignore empty or blank line
                {
                    string[] inputs = line.Split(new[] { splitter }, 2);
                    try
                    {
                        int index;
                        for (index = 0; index < classTypes.Length; index++)
                        {
                            if (classTypes[index].Name.Equals(inputs[0]))
                            {
                                break;
                            }
                        }
                        if (index < classTypes.Length) // found right class
                        {
                            T t;
                            //Console.WriteLine(classNames[index]);
                            Type classT = Type.GetType(classTypes[index].AssemblyQualifiedName);
                            //Console.WriteLine(classT);
                            Type[] paramTypes = new Type[1];
                            paramTypes[0] = typeof(string);
                            ConstructorInfo constructorT = classT.GetConstructor(paramTypes);
                            t = (T)constructorT.Invoke(new object[] { inputs[1] });
                            records.Add(t);
                        }
                        else
                        {
                            Log.WriteLine($"Error @ line #{count}: Type is not supported");
                        }
                    }
                    catch (TargetInvocationException ex)
                    //when (ex.InnerException is InvalidParameterException)
                    {
                        Log.WriteLine($"Error @ line #{count}:" + ex.InnerException.Message);
                    }
                    catch (Exception ex)
                        when ((ex is ArgumentException) || (ex is ArgumentNullException) ||
                              (ex is MemberAccessException) || (ex is MethodAccessException) ||
                              (ex is ArgumentException) || (ex is TargetParameterCountException) ||
                              (ex is NotSupportedException) || (ex is SecurityException))
                    {
                        Log.WriteLine($"Error @ line #{count}:" + ex.Message);
                    }
                }
            }
            return records;
        }
        public static void saveToFileWithType(string filePath, List<T> records, string ToStringMethodName)
        {
            try
            {
                int count = 0;
                string[] dataLines = new string[records.Count];
                foreach (var t in records)
                {
                    Type classT = t.GetType(); // must use instance.getType to get right implementation instead the one of base
                    MethodInfo ToStringMethod = classT.GetMethod(ToStringMethodName);
                    dataLines[count++] = (string)ToStringMethod.Invoke(t, new object[] { });
                }
                saveLinesToFile(filePath, dataLines);
            }
            catch (Exception ex)
                when ((ex is AmbiguousMatchException) || (ex is ArgumentNullException))
            {
                Log.WriteLine(ex.Message);
            }
        }
    }

    public class Util{
        public static bool parseToDateTime(string sDateTime, string format, out DateTime datetime)
        {
            DateTimeStyles styles = DateTimeStyles.AssumeLocal;
            CultureInfo culture = CultureInfo.InvariantCulture;
            return DateTime.TryParseExact(sDateTime, format, culture, styles, out datetime);
        }

        public static bool parseToDateTime(string sDateTime, out DateTime datetime)
        {
            DateTimeStyles styles = DateTimeStyles.AssumeLocal;
            CultureInfo culture = CultureInfo.CurrentCulture;
            return DateTime.TryParse(sDateTime, culture, styles, out datetime);
        }

        //Usage of IFormatProvider:
        //culture = CultureInfo.CreateSpecificCulture("fr-FR");
        //culture = CultureInfo.CreateSpecificCulture("en-US");
        public static bool parseToDateTime(string sDateTime, IFormatProvider culture, out DateTime datetime)
        {
            DateTimeStyles styles = DateTimeStyles.AssumeLocal;
            return DateTime.TryParse(sDateTime, culture, styles, out datetime);
        }

    }
}