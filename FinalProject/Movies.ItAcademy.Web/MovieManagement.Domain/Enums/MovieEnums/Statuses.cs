namespace MovieManagement.Domain.Enums
{
    public static class Statuses
    {
        public const string Uploaded = "Uploaded";//როცა მოდერატორმა ატვირთა და ელოდება ადმინის დასტურს
        public const string Published = "Published";//როცა ადმინმა დაადასტურა
        public const string Starting = "Starting";//დაწყებამდე დარჩა ერთი საათი
        public const string Ongoing = "Ongoing";//მიმდინარე ფილმი, გადის ლაივ რეჟიმში
        public const string Ended = "Ended";//დასრულებული ფილმი3
        public const string Deleted = "Deleted"; //მოდერატორის ან ადმინის მიერ წაშლილი3
        public const string Archived = "Archived";//დასრულების შემდეგ გადადის არქივში ავტომატურად3 ამათი გაერთიანება შეიძლება
      
    }
}
