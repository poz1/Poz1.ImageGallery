using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Poz1.ImageGallery
{
    class CarouselViewBehavior : Behavior<CarouselView>
    {
        public CarouselView AssociatedObject { get; private set; }

        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(CarouselViewBehavior), null);
      
        public ICommand PositionChangedCommand
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        protected override void OnAttachedTo(CarouselView bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;
            bindable.BindingContextChanged += OnBindingContextChanged;
            bindable.PositionSelected += Bindable_PositionSelected;
        }

        protected override void OnDetachingFrom(CarouselView bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.BindingContextChanged -= OnBindingContextChanged;
            bindable.PositionSelected -= Bindable_PositionSelected;
            AssociatedObject = null;
        }

        void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }


        private void Bindable_PositionSelected(object sender, SelectedPositionChangedEventArgs e)
        {
            if (PositionChangedCommand == null)
            {
                return;
            }
            if (PositionChangedCommand.CanExecute((int)e.SelectedPosition))
            {
                PositionChangedCommand.Execute((int)e.SelectedPosition);
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }
    }

}
