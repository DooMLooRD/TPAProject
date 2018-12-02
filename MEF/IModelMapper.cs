namespace MEF
{
    public interface IModelMapper<TUpModel,TDownModel>
    {
        TUpModel MapUp(TDownModel model);
        TDownModel MapDown(TUpModel model);
    }
}
