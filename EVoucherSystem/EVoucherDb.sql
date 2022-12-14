PGDMP                         z         
   EVoucherDb    12.7    12.7                0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false                       0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false                       0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false                       1262    74817 
   EVoucherDb    DATABASE     ?   CREATE DATABASE "EVoucherDb" WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'English_United States.1252' LC_CTYPE = 'English_United States.1252';
    DROP DATABASE "EVoucherDb";
                postgres    false            ?            1259    74887 	   Evouchers    TABLE     ?  CREATE TABLE public."Evouchers" (
    "Id" integer NOT NULL,
    "EVoucherCode" uuid NOT NULL,
    "Title" text NOT NULL,
    "Description" text NOT NULL,
    "ExpiredDate" timestamp with time zone NOT NULL,
    "Image" text NOT NULL,
    "Amount" numeric NOT NULL,
    "MaxBuyLimit" integer NOT NULL,
    "GiftPerUserLimit" integer NOT NULL,
    "IsActive" boolean NOT NULL,
    "IsDeleted" boolean NOT NULL
);
    DROP TABLE public."Evouchers";
       public         heap    postgres    false            ?            1259    74885    Evouchers_Id_seq    SEQUENCE     ?   ALTER TABLE public."Evouchers" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."Evouchers_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    203            ?            1259    74897    Purchase    TABLE     W  CREATE TABLE public."Purchase" (
    "Id" integer NOT NULL,
    "EVoucherId" integer NOT NULL,
    "PromoCode" text NOT NULL,
    "Qty" integer NOT NULL,
    "Amount" numeric NOT NULL,
    "BuyType" integer NOT NULL,
    "PaymentMethod" integer NOT NULL,
    "Name" text NOT NULL,
    "Phno" text NOT NULL,
    "IsDeleted" boolean NOT NULL
);
    DROP TABLE public."Purchase";
       public         heap    postgres    false            ?            1259    74895    Purchase_Id_seq    SEQUENCE     ?   ALTER TABLE public."Purchase" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."Purchase_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    205            
          0    74887 	   Evouchers 
   TABLE DATA           ?   COPY public."Evouchers" ("Id", "EVoucherCode", "Title", "Description", "ExpiredDate", "Image", "Amount", "MaxBuyLimit", "GiftPerUserLimit", "IsActive", "IsDeleted") FROM stdin;
    public          postgres    false    203   ?                 0    74897    Purchase 
   TABLE DATA           ?   COPY public."Purchase" ("Id", "EVoucherId", "PromoCode", "Qty", "Amount", "BuyType", "PaymentMethod", "Name", "Phno", "IsDeleted") FROM stdin;
    public          postgres    false    205   ?                  0    0    Evouchers_Id_seq    SEQUENCE SET     A   SELECT pg_catalog.setval('public."Evouchers_Id_seq"', 1, false);
          public          postgres    false    202                       0    0    Purchase_Id_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public."Purchase_Id_seq"', 1, false);
          public          postgres    false    204            ?
           2606    74894    Evouchers PK_Evouchers 
   CONSTRAINT     Z   ALTER TABLE ONLY public."Evouchers"
    ADD CONSTRAINT "PK_Evouchers" PRIMARY KEY ("Id");
 D   ALTER TABLE ONLY public."Evouchers" DROP CONSTRAINT "PK_Evouchers";
       public            postgres    false    203            ?
           2606    74904    Purchase PK_Purchase 
   CONSTRAINT     X   ALTER TABLE ONLY public."Purchase"
    ADD CONSTRAINT "PK_Purchase" PRIMARY KEY ("Id");
 B   ALTER TABLE ONLY public."Purchase" DROP CONSTRAINT "PK_Purchase";
       public            postgres    false    205            
   ?   x?}ϻNA???[?C^?3???!T4?XH!???4QB?-}?.C?{?ig)?"?X?Ol?gҽ??L?#????x??(dB??B?Tcژ?Y?_?????????/??????;J??e?`??<|?jz?=yd????xc"DoY?5qBQأ?Xt??̀?6?/(??U?J??_{?.?l˲? Z{J,         ~   x?%̱
1???S?H??M:??Y\\T8?n????I ??'"+?4???????????B?Ē??`?P4N???x???{[ǚ?̵c????Q籭?^???????<?W?"\???.???B     