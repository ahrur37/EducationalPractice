using System;
using System.Collections.Generic;
using Avalonia.Controls;

namespace EducationalPractice
{

    public class NavigationService
    {

        private static readonly Stack<UserControl> _backStack = new();
        private static ContentControl _currentContainer;

        public static event Action<UserControl> OnNavigation;
        public static event Action<bool> OnBackStackChanged;

        public static void Initialize(ContentControl container)
        {
            _currentContainer = container;
        }

        public static void NavigateTo(UserControl view)
        {
            if (_currentContainer?.Content is UserControl currentView && currentView != view)
            {
                _backStack.Push(currentView);
            }

            _currentContainer.Content = view;
            OnNavigation?.Invoke(view);
            OnBackStackChanged?.Invoke(_backStack.Count > 0);
        }

        public static void NavigateTo<T>() where T : UserControl, new()
        {
            NavigateTo(new T());
        }

        public static void GoBack()
        {
            if (_backStack.Count > 0)
            {
                _currentContainer.Content = _backStack.Pop();
                OnBackStackChanged?.Invoke(_backStack.Count > 0);
            }
        }

        public static bool CanGoBack => _backStack.Count > 0;
    }
}