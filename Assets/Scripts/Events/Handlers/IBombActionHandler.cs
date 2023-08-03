namespace Events.Handlers
{
    public interface IBombActionHandler : IGlobalSubscriber
    {
        void HandleEquipBomb();
        void HandleDropBomb();
        void HandlePlantBomb();
        void HandleDefuseBomb();
    }
}