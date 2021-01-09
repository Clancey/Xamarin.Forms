namespace Xamarin.Forms
{
	internal interface IFlowDirectionController
	{
		EffectiveFlowDirection EffectiveFlowDirection { get; set; }

		float Width { get; }

		bool ApplyEffectiveFlowDirectionToChildContainer { get; }
	}
}