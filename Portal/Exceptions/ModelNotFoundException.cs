namespace Portal.Exceptions
{
    public class ModelNotFoundException : BasePortalException
    {
        public ModelNotFoundException(string message) : base(message)
        {
        }

        public ModelNotFoundException(string entity, string id) : base($"Entity {entity} with id {id} was not found.")
        {
        }
    }

    public class ModelNotFoundException<T> : ModelNotFoundException
    {
        public ModelNotFoundException() : base($"Entity {typeof(T).Name} was not found.")
        {
        }

        public ModelNotFoundException(string id) : base($"Entity {typeof(T).Name} with id {id} was not found.")
        {
        }
    }
}
