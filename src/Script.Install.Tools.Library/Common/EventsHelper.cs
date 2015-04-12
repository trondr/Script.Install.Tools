// © 2005 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Script.Install.Tools.Library.Common
{
   public static class EventsHelper
   {
      delegate void AsyncFire(Delegate del, object[] args);

      static void InvokeDelegate(Delegate del, object[] args)
      {
         var synchronizer = del.Target as ISynchronizeInvoke;
         if (synchronizer != null)//Requires thread affinity
         {
            if (synchronizer.InvokeRequired)
            {
               synchronizer.Invoke(del, args);
               return;
            }
         }
         //Not requiring thread afinity or invoke is not required
         del.DynamicInvoke(args);
      }
      [MethodImpl(MethodImplOptions.NoInlining)]
      public static void UnsafeFire(Delegate del, params object[] args)
      {
         if (args.Length > 7)
         {
            Trace.TraceWarning("Too many parameters. Consider a structure to enable the use of the type-safe versions");
         }
         if (del == null)
         {
            return;
         }
         var delegates = del.GetInvocationList();

         foreach (var sink in delegates)
         {
            try
            {
               InvokeDelegate(sink, args);
            }
            catch (Exception ex)
            {
               Trace.TraceError("Failed to invoke delegate. Error: " + ex.Message);
            }
         }
      }
      [MethodImpl(MethodImplOptions.NoInlining)]
      public static void UnsafeFireInParallel(Delegate del, params object[] args)
      {
         if (args.Length > 7)
         {
            Trace.TraceWarning("Too many parameters. Consider a structure to enable the use of the type-safe versions");
         }
         if (del == null)
         {
            return;
         }
         var delegates = del.GetInvocationList();

         var calls = new List<WaitHandle>(delegates.Length);
         AsyncFire asyncFire = InvokeDelegate;

         foreach (var sink in delegates)
         {
            var asyncResult = asyncFire.BeginInvoke(sink, args, null, null);
            calls.Add(asyncResult.AsyncWaitHandle);
         }
         var handles = calls.ToArray();
         WaitHandle.WaitAll(handles);
         Action<WaitHandle> close = handle => handle.Close();
         Array.ForEach(handles, close);
      }
      [MethodImpl(MethodImplOptions.NoInlining)]
      public static void UnsafeFireAsync(Delegate del, params object[] args)
      {
         if (args.Length > 7)
         {
            Trace.TraceWarning("Too many parameters. Consider a structure to enable the use of the type-safe versions");
         }
         if (del == null)
         {
            return;
         }
         var delegates = del.GetInvocationList();
         AsyncFire asyncFire = InvokeDelegate;
         AsyncCallback cleanUp = asyncResult => asyncResult.AsyncWaitHandle.Close();
         foreach (var sink in delegates)
         {
            asyncFire.BeginInvoke(sink, args, cleanUp, null);
         }
      }
      public static void Fire(EventHandler del, object sender, EventArgs e)
      {
         UnsafeFire(del, sender, e);
      }
      public static void Fire<T>(EventHandler<T> del, object sender, T t) where T : EventArgs
      {
         UnsafeFire(del, sender, t);
      }
      public static void Fire(GenericEventHandler del)
      {
         UnsafeFire(del);
      }
      public static void Fire<T>(GenericEventHandler<T> del, T t)
      {
         UnsafeFire(del, t);
      }
      public static void Fire<T, TU>(GenericEventHandler<T, TU> del, T t, TU u)
      {
         UnsafeFire(del, t, u);
      }
      public static void Fire<T, TU, TV>(GenericEventHandler<T, TU, TV> del, T t, TU u, TV v)
      {
         UnsafeFire(del, t, u, v);
      }
      public static void Fire<T, TU, TV, TW>(GenericEventHandler<T, TU, TV, TW> del, T t, TU u, TV v, TW w)
      {
         UnsafeFire(del, t, u, v, w);
      }
      public static void Fire<T, TU, TV, TW, TX>(GenericEventHandler<T, TU, TV, TW, TX> del, T t, TU u, TV v, TW w, TX x)
      {
         UnsafeFire(del, t, u, v, w, x);
      }
      public static void Fire<T, TU, TV, TW, TX, TY>(GenericEventHandler<T, TU, TV, TW, TX, TY> del, T t, TU u, TV v, TW w, TX x, TY y)
      {
         UnsafeFire(del, t, u, v, w, x, y);
      }
      public static void Fire<T, TU, TV, TW, TX, TY, TZ>(GenericEventHandler<T, TU, TV, TW, TX, TY, TZ> del, T t, TU u, TV v, TW w, TX x, TY y, TZ z)
      {
         UnsafeFire(del, t, u, v, w, x, y, z);
      }

      public static void FireInParallel(EventHandler del, object sender, EventArgs e)
      {
         UnsafeFireInParallel(del, sender, e);
      }
      public static void FireInParallel<T>(EventHandler<T> del, object sender, T t) where T : EventArgs
      {
         UnsafeFireInParallel(del, sender, t);
      }
      public static void FireInParallel(GenericEventHandler del)
      {
         UnsafeFireInParallel(del);
      }
      public static void FireInParallel<T>(GenericEventHandler<T> del, T t)
      {
         UnsafeFireInParallel(del, t);
      }
      public static void FireInParallel<T, TU>(GenericEventHandler<T, TU> del, T t, TU u)
      {
         UnsafeFireInParallel(del, t, u);
      }
      public static void FireInParallel<T, TU, TV>(GenericEventHandler<T, TU, TV> del, T t, TU u, TV v)
      {
         UnsafeFireInParallel(del, t, u, v);
      }
      public static void FireInParallel<T, TU, TV, TW>(GenericEventHandler<T, TU, TV, TW> del, T t, TU u, TV v, TW w)
      {
         UnsafeFireInParallel(del, t, u, v, w);
      }
      public static void FireInParallel<T, TU, TV, TW, TX>(GenericEventHandler<T, TU, TV, TW, TX> del, T t, TU u, TV v, TW w, TX x)
      {
         UnsafeFireInParallel(del, t, u, v, w, x);
      }
      public static void FireInParallel<T, TU, TV, TW, TX, TY>(GenericEventHandler<T, TU, TV, TW, TX, TY> del, T t, TU u, TV v, TW w, TX x, TY y)
      {
         UnsafeFireInParallel(del, t, u, v, w, x, y);
      }
      public static void FireInParallel<T, TU, TV, TW, TX, TY, TZ>(GenericEventHandler<T, TU, TV, TW, TX, TY, TZ> del, T t, TU u, TV v, TW w, TX x, TY y, TZ z)
      {
         UnsafeFireInParallel(del, t, u, v, w, x, y, z);
      }

      public static void FireAsync(EventHandler del, object sender, EventArgs e)
      {
         UnsafeFireAsync(del, sender, e);
      }
      public static void FireAsync<T>(EventHandler<T> del, object sender, T t) where T : EventArgs
      {
         UnsafeFireAsync(del, sender, t);
      }
      public static void FireAsync(GenericEventHandler del)
      {
         UnsafeFireAsync(del);
      }
      public static void FireAsync<T>(GenericEventHandler<T> del, T t)
      {
         UnsafeFireAsync(del, t);
      }
      public static void FireAsync<T, TU>(GenericEventHandler<T, TU> del, T t, TU u)
      {
         UnsafeFireAsync(del, t, u);
      }
      public static void FireAsync<T, TU, TV>(GenericEventHandler<T, TU, TV> del, T t, TU u, TV v)
      {
         UnsafeFireAsync(del, t, u, v);
      }
      public static void FireAsync<T, TU, TV, TW>(GenericEventHandler<T, TU, TV, TW> del, T t, TU u, TV v, TW w)
      {
         UnsafeFireAsync(del, t, u, v, w);
      }
      public static void FireAsync<T, TU, TV, TW, TX>(GenericEventHandler<T, TU, TV, TW, TX> del, T t, TU u, TV v, TW w, TX x)
      {
         UnsafeFireAsync(del, t, u, v, w, x);
      }
      public static void FireAsync<T, TU, TV, TW, TX, TY>(GenericEventHandler<T, TU, TV, TW, TX, TY> del, T t, TU u, TV v, TW w, TX x, TY y)
      {
         UnsafeFireAsync(del, t, u, v, w, x, y);
      }
      public static void FireAsync<T, TU, TV, TW, TX, TY, TZ>(GenericEventHandler<T, TU, TV, TW, TX, TY, TZ> del, T t, TU u, TV v, TW w, TX x, TY y, TZ z)
      {
         UnsafeFireAsync(del, t, u, v, w, x, y, z);
      }
   }
}