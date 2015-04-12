// © 2005 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

namespace Script.Install.Tools.Library.Common
{
    public delegate void GenericEventHandler();
    public delegate void GenericEventHandler<in T>(T t);
    public delegate void GenericEventHandler<in T, in TU>(T t, TU u);
    public delegate void GenericEventHandler<in T, in TU, in TV>(T t, TU u, TV v);
    public delegate void GenericEventHandler<in T, in TU, in TV, in TW>(T t, TU u, TV v, TW w);
    public delegate void GenericEventHandler<in T, in TU, in TV, in TW, in TX>(T t, TU u, TV v, TW w, TX x);
    public delegate void GenericEventHandler<in T, in TU, in TV, in TW, in TX, in TY>(T t, TU u, TV v, TW w, TX x, TY y);
    public delegate void GenericEventHandler<in T, in TU, in TV, in TW, in TX, in TY, in TZ>(T t, TU u, TV v, TW w, TX x, TY y, TZ z);
}