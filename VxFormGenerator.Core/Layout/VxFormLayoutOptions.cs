namespace VxFormGenerator.Core.Layout
{
    public class VxFormLayoutOptions
    {
        /// <summary>
        /// Set the label position for the form
        /// </summary>
        public LabelOrientation LabelOrientation { get; set; } = LabelOrientation.LEFT;
        /// <summary>
        /// Set the placeholder policy for the form
        /// </summary>
        public PlaceholderPolicy ShowPlaceholder { get; set; } = PlaceholderPolicy.EXPLICIT;
        /// <summary>
        /// Set the trigger for showing validation state
        /// </summary>
        public VisualFeedbackValidationPolicy VisualValidationPolicy { get; set; } = VisualFeedbackValidationPolicy.ONLY_INVALID;
    }
}
