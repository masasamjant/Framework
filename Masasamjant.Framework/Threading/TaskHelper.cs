namespace Masasamjant.Threading
{
    /// <summary>
    /// Provides task related helper methods.
    /// </summary>
    public static class TaskHelper
    {
        /// <summary>
        /// Convert <see cref="Action"/> to be executed in task.
        /// </summary>
        /// <param name="action">The <see cref="Action"/>.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        public static async Task ToTask(Action action)
        {
            await Task.Factory.StartNew(action);
        }

        /// <summary>
        /// Convert <see cref="Action{T1}"/> to be executed in task.
        /// </summary>
        /// <typeparam name="T1">The type of first parameter.</typeparam>
        /// <param name="action">The <see cref="Action{T1}"/>.</param>
        /// <param name="arg1">The first argument.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        public static async Task ToTask<T1>(this Action<T1> action, T1 arg1)
        {
            await Task.Factory.StartNew(() => {
                action(arg1);
            });
        }

        /// <summary>
        /// Convert <see cref="Action{T1, T2}"/> to be executed in task.
        /// </summary>
        /// <typeparam name="T1">The type of first parameter.</typeparam>
        /// <typeparam name="T2">The type of second parameter.</typeparam>
        /// <param name="action">The <see cref="Action{T1, T2}"/>.</param>
        /// <param name="arg1">The first argument.</param>
        /// <param name="arg2">The second argument.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        public static async Task ToTask<T1, T2>(this Action<T1, T2> action, T1 arg1, T2 arg2)
        {
            await Task.Factory.StartNew(() => {
                action(arg1, arg2);
            });
        }

        /// <summary>
        /// Convert <see cref="Action{T1, T2, T3}"/> to be executed in task.
        /// </summary>
        /// <typeparam name="T1">The type of first parameter.</typeparam>
        /// <typeparam name="T2">The type of second parameter.</typeparam>
        /// <typeparam name="T3">The type of third parameter.</typeparam>
        /// <param name="action">The <see cref="Action{T1, T2, T3}"/>.</param>
        /// <param name="arg1">The first argument.</param>
        /// <param name="arg2">The second argument.</param>
        /// <param name="arg3">The third argument.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        public static async Task ToTask<T1, T2, T3>(this Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
        {
            await Task.Factory.StartNew(() => {
                action(arg1, arg2, arg3);
            });
        }

        /// <summary>
        /// Convert <see cref="Action{T1, T2, T3, T4}"/> to be executed in task.
        /// </summary>
        /// <typeparam name="T1">The type of first parameter.</typeparam>
        /// <typeparam name="T2">The type of second parameter.</typeparam>
        /// <typeparam name="T3">The type of third parameter.</typeparam>
        /// <typeparam name="T4">The type of fourth parameter.</typeparam>
        /// <param name="action">The <see cref="Action{T1, T2, T3, T4}"/>.</param>
        /// <param name="arg1">The first argument.</param>
        /// <param name="arg2">The second argument.</param>
        /// <param name="arg3">The third argument.</param>
        /// <param name="arg4">The fourth argument.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        public static async Task ToTask<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            await Task.Factory.StartNew(() => {
                action(arg1, arg2, arg3, arg4);
            });
        }

        /// <summary>
        /// Convert <see cref="Action{T1, T2, T3, T4, T5}"/> to be executed in task.
        /// </summary>
        /// <typeparam name="T1">The type of first parameter.</typeparam>
        /// <typeparam name="T2">The type of second parameter.</typeparam>
        /// <typeparam name="T3">The type of third parameter.</typeparam>
        /// <typeparam name="T4">The type of fourth parameter.</typeparam>
        /// <typeparam name="T5">The type of fifth parameter.</typeparam>
        /// <param name="action">The <see cref="Action{T1, T2, T3, T4, T5}"/>.</param>
        /// <param name="arg1">The first argument.</param>
        /// <param name="arg2">The second argument.</param>
        /// <param name="arg3">The third argument.</param>
        /// <param name="arg4">The fourth argument.</param>
        /// <param name="arg5">The fifth argument.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        public static async Task ToTask<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            await Task.Factory.StartNew(() => {
                action(arg1, arg2, arg3, arg4, arg5);
            });
        }

        /// <summary>
        /// Convert <see cref="Func{TResult}"/> to be executed in task.
        /// </summary>
        /// <typeparam name="TResult">The type of result.</typeparam>
        /// <param name="func">The <see cref="Func{TResult}"/>.</param>
        /// <returns>A <see cref="Task{TResult}"/>.</returns>
        public static async Task<TResult> ToTask<TResult>(this Func<TResult> func)
        {
            return await Task.Factory.StartNew(func);
        }

        /// <summary>
        /// Convert <see cref="Func{T1, TResult}"/> to be executed in task.
        /// </summary>
        /// <typeparam name="TResult">The type of result.</typeparam>
        /// <typeparam name="T1">The type of first parameter.</typeparam>
        /// <param name="func">The <see cref="Func{T1, TResult}"/>.</param>
        /// <param name="arg1">The first argument</param>
        /// <returns>A <see cref="Task{TResult}"/>.</returns>
        public static async Task<TResult> ToTask<TResult, T1>(this Func<T1, TResult> func, T1 arg1)
        {
            return await Task.Factory.StartNew(() => { return func(arg1); });
        }

        /// <summary>
        /// Convert <see cref="Func{T1, T2, TResult}"/> to be executed in task.
        /// </summary>
        /// <typeparam name="TResult">The type of result.</typeparam>
        /// <typeparam name="T1">The type of first parameter.</typeparam>
        /// <typeparam name="T2">The type of second parameter.</typeparam>
        /// <param name="func">The <see cref="Func{T1, T2, TResult}"/>.</param>
        /// <param name="arg1">The first argument</param>
        /// <param name="arg2">The second argument.</param>
        /// <returns>A <see cref="Task{TResult}"/>.</returns>
        public static async Task<TResult> ToTask<TResult, T1, T2>(this Func<T1, T2, TResult> func, T1 arg1, T2 arg2)
        {
            return await Task.Factory.StartNew(() => { return func(arg1, arg2); });
        }

        /// <summary>
        /// Convert <see cref="Func{T1, T2, T3, TResult}"/> to be executed in task.
        /// </summary>
        /// <typeparam name="TResult">The type of result.</typeparam>
        /// <typeparam name="T1">The type of first parameter.</typeparam>
        /// <typeparam name="T2">The type of second parameter.</typeparam>
        /// <typeparam name="T3">The type of third parameter.</typeparam>
        /// <param name="func">The <see cref="Func{T1, T2, T3, TResult}"/>.</param>
        /// <param name="arg1">The first argument</param>
        /// <param name="arg2">The second argument.</param>
        /// <param name="arg3">The third argument.</param>
        /// <returns>A <see cref="Task{TResult}"/>.</returns>
        public static async Task<TResult> ToTask<TResult, T1, T2, T3>(this Func<T1, T2, T3, TResult> func, T1 arg1, T2 arg2, T3 arg3)
        {
            return await Task.Factory.StartNew(() => { return func(arg1, arg2, arg3); });
        }

        /// <summary>
        /// Convert <see cref="Func{T1, T2, T3, T4, TResult}"/> to be executed in task.
        /// </summary>
        /// <typeparam name="TResult">The type of result.</typeparam>
        /// <typeparam name="T1">The type of first parameter.</typeparam>
        /// <typeparam name="T2">The type of second parameter.</typeparam>
        /// <typeparam name="T3">The type of third parameter.</typeparam>
        /// <typeparam name="T4">The type of fourth parameter.</typeparam>
        /// <param name="func">The <see cref="Func{T1, T2, T3, T4, TResult}"/>.</param>
        /// <param name="arg1">The first argument</param>
        /// <param name="arg2">The second argument.</param>
        /// <param name="arg3">The third argument.</param>
        /// <param name="arg4">The fourth argument.</param>
        /// <returns>A <see cref="Task{TResult}"/>.</returns>
        public static async Task<TResult> ToTask<TResult, T1, T2, T3, T4>(this Func<T1, T2, T3, T4, TResult> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            return await Task.Factory.StartNew(() => { return func(arg1, arg2, arg3, arg4); });
        }

        /// <summary>
        /// Convert <see cref="Func{T1, T2, T3, T4, T5, TResult}"/> to be executed in task.
        /// </summary>
        /// <typeparam name="TResult">The type of result.</typeparam>
        /// <typeparam name="T1">The type of first parameter.</typeparam>
        /// <typeparam name="T2">The type of second parameter.</typeparam>
        /// <typeparam name="T3">The type of third parameter.</typeparam>
        /// <typeparam name="T4">The type of fourth parameter.</typeparam>
        /// <typeparam name="T5">The type of fifth parameter.</typeparam>
        /// <param name="func">The <see cref="Func{T1, T2, T3, T4, T5, TResult}"/>.</param>
        /// <param name="arg1">The first argument</param>
        /// <param name="arg2">The second argument.</param>
        /// <param name="arg3">The third argument.</param>
        /// <param name="arg4">The fourth argument.</param>
        /// <param name="arg5">The fifth argument.</param>
        /// <returns>A <see cref="Task{TResult}"/>.</returns>
        public static async Task<TResult> ToTask<TResult, T1, T2, T3, T4, T5>(this Func<T1, T2, T3, T4, T5, TResult> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            return await Task.Factory.StartNew(() => { return func(arg1, arg2, arg3, arg4, arg5); });
        }
    }
}
