using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FactoryGuardian.Models 
{
	// WPF 데이터에 묶여서 ui에 바로 갱신 (>> INotifyPropertyChanged <<)
    public class BaseModel : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler? PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(
				this,
				new PropertyChangedEventArgs(propertyName));
		}
		// 프로퍼티즈 변경 및 알려주는 역할
		protected bool SetProperty<T>(
			ref T field,
			T value, [CallerMemberName] string propertyName = "")
		{
			if (Equals(field, value))
				return false;

			field = value;
			OnPropertyChanged(propertyName);

			return true;
		}
	}
}