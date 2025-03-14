﻿namespace api_painel_producao.Models {
    public class FramedArtwork {

        public int Id { get; set; }

        public float Height { get; set; }
        public float Width { get; set; }

        public decimal TotalPrice { get; set; }

        public byte[]? ImageFile { get; set; }
        public string ImageFilePath { get; set; } = string.Empty;
        public string ImageDescription { get; set; } = string.Empty;

        public int? OrderId { get; set; }
        public Order? Order { get; set; }

        public Material? Glass { get; set; }
        public Material? Frame { get; set; }
        public Material? Background { get; set; }
        public Material? Paper { get; set; }
    }
}
