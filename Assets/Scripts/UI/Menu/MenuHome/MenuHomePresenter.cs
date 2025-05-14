namespace DefaultNamespace
{
    public class MenuHomePresenter
    {
        public MenuHomePresenter(MenuHomeView view, MenuPresenter menu)
        {
            view.ChooseHeroButton.onClick.AddListener(menu.ShowHeroSelect);
        }
    }
}