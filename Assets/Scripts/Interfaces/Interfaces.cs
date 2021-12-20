public interface IPopulationElement
{
	public void Mutate(float i_MutationRate);
	public float Fitness { get; set; }

}
