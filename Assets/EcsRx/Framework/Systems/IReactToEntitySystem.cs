using System;
using EcsRx.Entities;
using UniRx;

namespace EcsRx.Systems
{
    public interface IReactToEntitySystem : ISystem
    {
        UniRx.IObservable<IEntity> ReactToEntity(IEntity entity);

        void Execute(IEntity entity);
    }
}