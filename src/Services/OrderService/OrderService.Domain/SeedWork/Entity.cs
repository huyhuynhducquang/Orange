using MediatR;

namespace OrderService.Domain.SeedWork
{
    public abstract class Entity
    {
        int? _requestedHashCode;
        int _id;
        public virtual int Id
        {
            get { return _id; }
            protected set { _id = value; }
        }

        public static bool operator ==(Entity left, Entity right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null));
            else
                return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }


        public bool IsTransient()
        {
            return this.Id == default(Int32);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity)) return false;
            if (Object.ReferenceEquals(this, obj)) return true;
            if (this.GetType() != obj.GetType()) return false;
            Entity item = (Entity)obj;
            if (item.IsTransient() || this.IsTransient()) return false;
            else return item.Id == this.Id;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = this.Id.GetHashCode();

                return _requestedHashCode.Value;
            }
            else return base.GetHashCode();
        }
    }
}
