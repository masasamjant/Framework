using System.Text;

namespace Masasamjant.Threading
{
    [TestClass]
    public class TaskHelperUnitTest : UnitTest
    {
        [TestMethod]
        public async Task Test_Create_Task_From_Action()
        {
            var builder = new StringBuilder();
            Action action1 = () => { builder.Append(":"); };
            Action<int> action2 = (x) => { builder.Append(x); };
            Action<int, int> action3 = (x, y) => { builder.Append(x); builder.Append(y); };
            Action<int, int, int> action4 = (x, y, z) => { builder.Append(x); builder.Append(y); builder.Append(z); };
            Action<int, int, int, int> action5 = (a, b, c, d) => { builder.Append(a); builder.Append(b); builder.Append(c); builder.Append(d); };
            Action<int, int, int, int, int> action6 = (a, b, c, d, e) => { builder.Append(a); builder.Append(b); builder.Append(c); builder.Append(d); builder.Append(e); };
            await TaskHelper.ToTask(action1);
            await TaskHelper.ToTask(action2, 1);
            await TaskHelper.ToTask(action3, 2, 3);
            await TaskHelper.ToTask(action4, 4, 5, 6);
            await TaskHelper.ToTask(action5, 7, 8, 9, 10);
            await TaskHelper.ToTask(action6, 11, 12, 13, 14, 15);
            string expected = ":123456789101112131415";
            string actual = builder.ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task Test_Create_Task_From_Func()
        {
            var builder = new StringBuilder();
            Func<int> func1 = () => { return 0; };
            Func<int, int> func2 = (x) => { return x + 1; };
            Func<int, int, int> func3 = (x, y) => { return x + y; };
            Func<int, int, int, int> func4 = (x, y, z) => { return x + y + z; };
            Func<int, int, int, int, int> func5 = (a, b, c, d) => { return a + b + c + d; };
            Func<int, int, int, int, int, int> func6 = (a, b, c, d, e) => { return a + b + c + d + e; };
            var a = await TaskHelper.ToTask(func1);       // 0
            var b = await TaskHelper.ToTask(func2, a);    // 1
            var c = await TaskHelper.ToTask(func3, a, b); // 1
            var d = await TaskHelper.ToTask(func4, a, b, c); // 2
            var e = await TaskHelper.ToTask(func5, a, b, c, d); // 4
            var f = await TaskHelper.ToTask(func6, a, b, c, d, e); // 8
            var expected = 0 + 1 + 1 + 2 + 4 + 8;
            var actual = a + b + c + d + e + f;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_Get_Result_As_Array()
        {
            var expected = new int[] { 1, 2, 3, 4, 5 };
            Func<IEnumerable<int>> func = () => { return (int[])expected.Clone(); };
            var task = TaskHelper.ToTask(func);
            var array = TaskHelper.ToArray(task);
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public async Task Test_Get_Result_As_ArrayAsync()
        {
            var expected = new int[] { 1, 2, 3, 4, 5 };
            Func<IEnumerable<int>> func = () => { return (int[])expected.Clone(); };
            var task = TaskHelper.ToTask(func);
            var array = await TaskHelper.ToArrayAsync(task);
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void Test_Get_Result_As_List()
        {
            var expected = new int[] { 1, 2, 3, 4, 5 };
            Func<IEnumerable<int>> func = () => { return (int[])expected.Clone(); };
            var task = TaskHelper.ToTask(func);
            var list = TaskHelper.ToList(task);
            CollectionAssert.AreEqual(expected, list);
        }

        [TestMethod]
        public async Task Test_Get_Result_As_ListAsync()
        {
            var expected = new int[] { 1, 2, 3, 4, 5 };
            Func<IEnumerable<int>> func = () => { return (int[])expected.Clone(); };
            var task = TaskHelper.ToTask(func);
            var list = await TaskHelper.ToListAsync(task);
            CollectionAssert.AreEqual(expected, list);
        }
    }
}
