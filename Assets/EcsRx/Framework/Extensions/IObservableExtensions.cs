using System;
using UniRx;

namespace EcsRx.Extensions
{
    public static class IObservableExtensions
    {
        public static UniRx.IObservable<Unit> AsTrigger<T>(this UniRx.IObservable<T> observable)
        { return observable.Select(x => Unit.Default); } 
    }
}