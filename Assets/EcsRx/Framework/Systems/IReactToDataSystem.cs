using System;
using EcsRx.Entities;
using EcsRx.Events;
using UniRx;

namespace EcsRx.Systems
{
    public interface IReactToDataSystem<T> : ISystem
    {
        UniRx.IObservable<T> ReactToData(IEntity entity);

        void Execute(IEntity entity, T reactionData);
    }
}