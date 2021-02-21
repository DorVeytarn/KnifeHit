using Utils.Singletone;

public class KnifePool : ObjectPool<Knife>
{
    private UserDataManager dataManager;

    private void Awake()
    {
        SceneComponentProvider.RegisterComponent(typeof(KnifePool), this);
    }

    private void Start()
    {
        dataManager = SceneComponentProvider.GetComponent(typeof(UserDataManager)) as UserDataManager;

        if(dataManager.CurrentKnife.Model != null)
            SetNewSpawnObject(dataManager.CurrentKnife.Model);
    }

    public override void CreateItems(int amount = 0)
    {
        base.CreateItems(amount);

        for (int i = 0; i < items.Count; i++)
        {
            items[i].AddSuccessCallback(() => dataManager.UpdateUserData(UDType.Score, 1));
        }
    }

    public void SetFirstKnive()
    {
        items[0].gameObject.SetActive(true);
    }
}
