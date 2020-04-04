using System;
using System.ComponentModel;
using System.Threading;
using System.Diagnostics;

namespace DeBox.Unitoolz.Extensions
{
    public static class SafeInvoker
    {
        #region Safe Event Invocation

        /// <summary>
        ///  makes sure the event is not null when invoking it
        /// </summary>
        public static void SafeInvoke<TEventArgs>(this EventHandler<TEventArgs> @event,
            Object sender, TEventArgs eventArgs) where TEventArgs : EventArgs
        {
            EventHandler<TEventArgs> e = Interlocked.CompareExchange(ref @event, null, null);
            if (e != null)
            {
                var invocationList = e.GetInvocationList();
                foreach (EventHandler<TEventArgs> handler in invocationList)
                {
                    try 
                    {
                        handler(sender, eventArgs);
                    }
                    catch(Exception ex) {
                        HandleExceptions(handler, ex);
                    }
                }
            }
        }

        /// <summary>
        ///  makes sure the event is not null when invoking it
        /// </summary>
        public static void SafeInvoke<TEventArgs>(this EventHandler<TEventArgs> @event, Object sender)
            where TEventArgs : EventArgs
        {
            EventHandler<TEventArgs> e = Interlocked.CompareExchange(ref @event, null, null);
            if (e != null)
            {
                var invocationList = e.GetInvocationList();
                foreach (EventHandler<TEventArgs> handler in invocationList) 
                {
                    try 
                    {
                        handler(sender, null);
                    }
                    catch (Exception ex)
                    {
                        HandleExceptions(handler, ex);
                    }
                }
            }
        }

        /// <summary>
        ///  makes sure the event is not null when invoking it
        /// </summary>
        public static void SafeInvoke<TEventArgs>(this EventHandler<TEventArgs> @event)
            where TEventArgs : EventArgs
        {
            EventHandler<TEventArgs> e = Interlocked.CompareExchange(ref @event, null, null);
            if (e != null)
            {
                var invocationList = e.GetInvocationList();
                foreach (EventHandler<TEventArgs> handler in invocationList)
                {
                    try
                    {
                        handler(null, null);
                    }
                    catch (Exception ex)
                    {
                        HandleExceptions(handler, ex);
                    }
                }
            }
        }

        /// <summary>
        ///  makes sure the event is not null when invoking it
        /// </summary>
        public static void SafeInvoke(this EventHandler @event,
            Object sender, EventArgs eventArgs)
        {
            EventHandler e = Interlocked.CompareExchange(ref @event, null, null);
            if (e != null)
            {
                var invocationList = e.GetInvocationList();
                foreach (EventHandler handler in invocationList)
                {
                    try
                    {
                        handler(sender, EventArgs.Empty);
                    }
                    catch (Exception ex)
                    {
                        HandleExceptions(handler, ex);
                    }
                }
            }
        }

        /// <summary>
        ///  makes sure the event is not null when invoking it
        /// </summary>
        public static void SafeInvoke(this EventHandler @event, Object sender)
        {
            EventHandler e = Interlocked.CompareExchange(ref @event, null, null);
            if (e != null)
            {
                var invocationList = e.GetInvocationList();
                foreach (EventHandler handler in invocationList)
                {
                    try
                    {
                        handler(sender, EventArgs.Empty);
                    }
                    catch (Exception ex)
                    {
                        HandleExceptions(handler, ex);
                    }
                }
            }
        }

        /// <summary>
        ///  makes sure the event is not null when invoking it
        /// </summary>
        public static void SafeInvoke(this EventHandler @event)
        {
            EventHandler e = Interlocked.CompareExchange(ref @event, null, null);
            if (e != null)
            {
                var invocationList = e.GetInvocationList();
                foreach (EventHandler handler in invocationList)
                {
                    try
                    {
                        handler(null, EventArgs.Empty);
                    }
                    catch (Exception ex)
                    {
                        HandleExceptions(handler, ex);
                    }
                }
            }
        }

        /// <summary>
        ///  makes sure the event is not null when invoking it
        /// </summary>
        public static void SafeInvoke<T, T2>(this Action<T, T2> @event, T arg1, T2 arg2)
        {
            Action<T, T2> e = Interlocked.CompareExchange(ref @event, null, null);
            if (e != null)
            {
                var invocationList = e.GetInvocationList();
                foreach (Action<T, T2> handler in invocationList)
                {
                    try
                    {
                        handler(arg1, arg2);
                    }
                    catch (Exception ex)
                    {
                        HandleExceptions(handler, ex);
                    }
                }
            }
        }

        /// <summary>
        ///  makes sure the event is not null when invoking it
        /// </summary>
        public static void SafeInvoke<T>(this Action<T> @event, T args)
        {
            Action<T> e = Interlocked.CompareExchange(ref @event, null, null);
            if (e != null)
            {
                var invocationList = e.GetInvocationList();
                foreach (Action<T> handler in invocationList)
                {
                    try
                    {
                        handler(args);
                    }
                    catch (Exception ex)
                    {
                        HandleExceptions(handler, ex);
                    }
                }
            }
        }

        /// <summary>
        ///  makes sure the event is not null when invoking it
        /// </summary>
        public static bool SafeInvoke<T>(this Func<T> @event, out T retVal)
        {
            Func<T> e = Interlocked.CompareExchange(ref @event, null, null);
            retVal = default(T);
            if (e != null) {
                var invocationList = e.GetInvocationList();

                if (invocationList.Length == 0) return false;

                var enumerator = invocationList.GetEnumerator();

                enumerator.MoveNext();

                try
                {
                    retVal = ((Func<T>)enumerator.Current).Invoke();
                }
                catch (Exception ex)
                {
                    HandleExceptions((Func<T>)enumerator.Current, ex);
                }

                while (enumerator.MoveNext())
                {
                    try
                    {
                        ((Func<T>)enumerator.Current).Invoke();
                    }
                    catch (Exception ex)
                    {
                        HandleExceptions((Func<T>)enumerator.Current, ex);
                    } 
                }
                return true;
            }
            return false;
        }

        /// <summary>
        ///  makes sure the event is not null when invoking it
        /// </summary>
        public static bool SafeInvoke<T,T2>(this Func<T,T2> @event,T arg, out T2 retVal)
        {
            Func<T,T2> e = Interlocked.CompareExchange(ref @event, null, null);
            retVal = default(T2);
            if (e != null)
            {
                var invocationList = e.GetInvocationList();

                if (invocationList.Length == 0) return false;

                var enumerator = invocationList.GetEnumerator();

                enumerator.MoveNext();

                try
                {
                    retVal = ((Func<T,T2>)enumerator.Current).Invoke(arg);
                }
                catch (Exception ex)
                {
                    HandleExceptions((Func<T, T2>)enumerator.Current, ex);
                }

                while (enumerator.MoveNext())
                {
                    try
                    {
                        ((Func<T, T2>)enumerator.Current).Invoke(arg);
                    }
                    catch (Exception ex)
                    {
                        HandleExceptions((Func<T, T2>)enumerator.Current, ex);
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        ///  makes sure the event is not null when invoking it
        /// </summary>
        public static void SafeInvoke<T>(this Action<T> @event)
        {
            Action<T> e = Interlocked.CompareExchange(ref @event, null, null);
            if (e != null)
            {
                var invocationList = e.GetInvocationList();
                foreach (Action<T> handler in invocationList)
                {
                    try
                    {
                        handler(default(T));
                    }
                    catch (Exception ex)
                    {
                        HandleExceptions(handler, ex);
                    }
                }
            }
        }

        /// <summary>
        ///  makes sure the event is not null when invoking it
        /// </summary>
        public static void SafeInvoke<T1, T2>(this Action<T1, T2> @event, T1 arg)
        {
            Action<T1, T2> e = Interlocked.CompareExchange(ref @event, null, null);
            if (e != null)
            {
                var invocationList = e.GetInvocationList();
                foreach (Action<T1, T2> handler in invocationList)
                {
                    try
                    {
                        handler(arg, default(T2));
                    }
                    catch (Exception ex)
                    {
                        HandleExceptions(handler, ex);
                    }
                }
            }
        }

        /// <summary>
        ///  makes sure the event is not null when invoking it
        /// </summary>
        public static void SafeInvoke<T1, T2>(this Action<T1, T2> @event)
        {
            Action<T1, T2> e = Interlocked.CompareExchange(ref @event, null, null);
            if (e != null)
            {
                var invocationList = e.GetInvocationList();
                foreach (Action<T1, T2> handler in invocationList)
                {
                    try
                    {
                        handler(default(T1), default(T2));
                    }
                    catch (Exception ex)
                    {
                        HandleExceptions(handler, ex);
                    }
                }
            }
        }

        /// <summary>
        ///  makes sure the event is not null when invoking it
        /// </summary>
        public static void SafeInvoke(this Action @event)
        {
            Action e = Interlocked.CompareExchange(ref @event, null, null);
            if (e != null)
            {
                var invocationList = e.GetInvocationList();
                foreach (Action handler in invocationList)
                {
                    try
                    {
                        handler();
                    }
                    catch (Exception ex) {
                        HandleExceptions(handler, ex);
                    }
                }
            }
        }


        /// <summary>
        ///  makes sure the event is not null when invoking it
        /// </summary>
        /// <remarks>for use in classes that implement INotifyPropertyChanged</remarks>
        public static void SafeInvoke(this PropertyChangedEventHandler @event, object sender, string propertyName)
        {
            PropertyChangedEventHandler e = Interlocked.CompareExchange(ref @event, null, null);
            if (e != null)
            {
                var args = new PropertyChangedEventArgs(propertyName);
                var invocationList = e.GetInvocationList();

                foreach (PropertyChangedEventHandler handler in invocationList)
                {
                    try
                    {
                        handler(sender, args);
                    }
                    catch (Exception ex)
                    {
                        HandleExceptions(handler, ex);
                    }
                }
            }
        }

        /// <summary>
        ///  makes sure the event is not null when invoking it
        /// </summary>
        /// <remarks>for invoking events with different signatures then the overloads</remarks>
        public static void SafeInvokeDynamic(this MulticastDelegate @event, params object[] args)
        {
            MulticastDelegate e = Interlocked.CompareExchange(ref @event, null, null);
            if (e != null)
            {
                var invocationList = e.GetInvocationList();
                foreach (Delegate handler in invocationList)
                {
                    try
                    {
                        handler.DynamicInvoke(args);
                    }
                    catch (Exception ex)
                    {
                        HandleExceptions(handler, ex);
                    }
                }
            }
        }

        private static void HandleExceptions(Delegate handler, Exception ex) {
            var method = handler.Method;

            var handlerName = method.DeclaringType.Name + "." + (method.Name.StartsWith("<", StringComparison.Ordinal) 
                ? method.Name.Substring(1, method.Name.Length - 6) 
                : method.Name) + "()";

            var innerEx = ex.GetBaseException();
            Debug.WriteLine("an event handler in " + handlerName + " that was SafeInvoked threw an " + innerEx.GetType().Name + ": " + innerEx);
        }

        #endregion
    }
}