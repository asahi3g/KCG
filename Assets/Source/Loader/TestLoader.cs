using System.Collections.Generic;
using System.Text;
using Enums;
using Item;

public static class TestLoader
{
    public enum StatusType
    {
        Success,
        Failed
    }
    
    public static Result Run()
    {
        
        ItemCreationApi itemCreationApi = RunItemCreationApi();
        
        
        StringBuilder message = new StringBuilder();
        message.Append(itemCreationApi.Message);
        
        return new Result()
        {
            Status = GetStatusType(itemCreationApi.Status),
            Message = message.ToString(),
            ItemCreationApi = itemCreationApi
        };
    }

    private static ItemCreationApi RunItemCreationApi()
    {
        ItemProperties[] itemPropertiesArray = GameState.ItemCreationApi.GetAll();
        int length = itemPropertiesArray.Length;
        StringBuilder messageSb = new StringBuilder();
        List<ItemCreationApi.InvalidItemProperties> invalidItemProperties = new List<ItemCreationApi.InvalidItemProperties>();
        
        for (int i = 0; i < length; i++)
        {
            ItemProperties itemProperties = itemPropertiesArray[i];

            bool labelOK = !string.IsNullOrEmpty(itemProperties.ItemLabel);
            bool itemTypeOK = itemProperties.ItemType != ItemType.Error;
            bool itemGroupeOK = itemProperties.Group != ItemGroupType.Error;

            if (!labelOK || !itemTypeOK || !itemGroupeOK)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"{nameof(ItemProperties)} index[{i.ToStringPretty()}]");
                
                if(!labelOK) sb.AppendLine($"\t{nameof(itemProperties.ItemLabel)} is null or empty");
                if(!itemTypeOK) sb.AppendLine($"\t{nameof(itemProperties.ItemType)}.{itemProperties.ItemType.ToStringPretty()} is invalid");
                if(!itemGroupeOK) sb.AppendLine($"\t{nameof(itemProperties.Group)}.{itemProperties.Group.ToStringPretty()} is invalid");

                string msg = sb.ToString();
                messageSb.Append(msg);

                ItemCreationApi.InvalidItemProperties invalid = new ItemCreationApi.InvalidItemProperties()
                {
                    Message = msg,
                    ItemProperties = itemProperties
                };
                
                invalidItemProperties.Add(invalid);
            }
        }

        return new ItemCreationApi()
        {
            Status = invalidItemProperties.Count == 0 ? StatusType.Success : StatusType.Failed,
            Message = messageSb.ToString(),
            InvalidItemPropertiesArray = invalidItemProperties.ToArray()
        };
    }

    private static StatusType GetStatusType(params StatusType[] statusTypes)
    {
        return new HashSet<StatusType>(statusTypes).Contains(StatusType.Failed)
            ? StatusType.Failed
            : StatusType.Success;
    }
    
    public class Result
    {
        public StatusType Status;
        public string Message;
        public ItemCreationApi ItemCreationApi;
    }

    public class ItemCreationApi
    {
        public StatusType Status;
        public string Message;
        public InvalidItemProperties[] InvalidItemPropertiesArray;
        
        public class InvalidItemProperties
        {
            public string Message;
            public ItemProperties ItemProperties;
        }
    }
}
