using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using Roslyn.Compilers;
using Roslyn.Scripting.CSharp;
using Roslyn.Scripting;
using Roslyn.Compilers.CSharp;
using System.IO;
using Testing.Core;
using System.Windows.Forms;
using System.Drawing;

namespace RoslynClassGenerator
{
    public class RoslynGen
    {
        public Form GetGenerator()
        {
            var sw = new StreamReader(@"c:\users\mitke\documents\visual studio 2010\Projects\WindowsFormsWithRoslyn\WindowsFormsWithRoslyn\TextFile1.txt");
            var sw3 = new StreamReader(@"C:\Users\mitke\documents\visual studio 2010\Projects\WindowsFormsWithRoslyn\WindowsFormsWithRoslyn\TextFile2.txt");
            var sw4 = new StreamReader(@"C:\Users\mitke\documents\visual studio 2010\Projects\WindowsFormsWithRoslyn\WindowsFormsWithRoslyn\TextFile3.txt");
           
            string formCodeFrontEnd = sw.ReadToEnd();
            string formCodeBackEnd = sw3.ReadToEnd();
            string program = sw4.ReadToEnd();

            SyntaxTree tree = SyntaxTree.ParseCompilationUnit(formCodeFrontEnd);
            SyntaxTree tree2 = SyntaxTree.ParseCompilationUnit(formCodeBackEnd);
            SyntaxTree tree3 = SyntaxTree.ParseCompilationUnit(program);

            var compilation = Compilation.Create("WindowsFormsWithRoslynTest",
                                 options: new CompilationOptions(OutputKind.WindowsApplication))
                  .AddReferences(new MetadataReference[]
                                   {
                                       new AssemblyFileReference(typeof (object).Assembly.Location),
                                       new AssemblyFileReference(typeof (Form).Assembly.Location),
                                       new AssemblyFileReference(typeof(Control).Assembly.Location),
                                       new AssemblyFileReference(typeof(Button).Assembly.Location),
                                       new AssemblyFileReference(typeof(Label).Assembly.Location),
                                       new AssemblyFileReference(typeof(NativeWindow).Assembly.Location),
                                       new AssemblyFileReference(typeof(ButtonBase).Assembly.Location),
                                       new AssemblyFileReference(typeof(MonthCalendar).Assembly.Location),
                                       new AssemblyFileReference(typeof(IContainer).Assembly.Location),
                                       new AssemblyFileReference(typeof(Enumerable).Assembly.Location),
                                       new AssemblyFileReference(typeof(IComponent).Assembly.Location),
                                       new AssemblyFileReference(typeof(Point).Assembly.Location), 
                                       new AssemblyFileReference(typeof(TextBox).Assembly.Location),
                                       new AssemblyFileReference(typeof(Size).Assembly.Location),
                                       new AssemblyFileReference(typeof(AutoScaleMode).Assembly.Location)

                                   }).AddSyntaxTrees(
                                   new SyntaxTree[]
                                   {   tree,
                                       tree2,tree3
                                   });

            Assembly assemblyTesting;

            using (var memStream = new MemoryStream())
            {
                var emitresult = compilation.Emit(memStream);
                assemblyTesting = Assembly.Load(memStream.GetBuffer());
            }

            var testingMacro = assemblyTesting.CreateInstance("WindowsFormsWithRoslynTest.FormTemplateTest", false) as Form;

            return testingMacro;
        }
    }
}
