namespace ASP_0112
{
    public class Result
    {
        public Fraction Frc { get; set; }
        public string Operation { get; set; }
        public Fraction Frc2 { get; set; }
        public Fraction Res { get; set; }
        public Result(Fraction frc, string operation, Fraction frc2, Fraction res)
        {
            Frc = frc;
            Operation = operation;
            Frc2 = frc2;
            Res = res;
        }
        public override bool Equals(object? obj)
        {
            return obj is Result other &&
                   EqualityComparer<Fraction>.Default.Equals(Frc, other.Frc) &&
                   Operation == other.Operation &&
                   EqualityComparer<Fraction>.Default.Equals(Frc2, other.Frc2) &&
                   EqualityComparer<Fraction>.Default.Equals(Res, other.Res);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Frc, Operation, Frc2, Res);
        }
        public override string ToString()
        {
            return $"{Frc} {Operation} {Frc2} = {Res}\tEquals {Res.decimalFract()}";
        }
    }
}
