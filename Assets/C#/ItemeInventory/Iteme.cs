using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iteme : MonoBehaviour
{
    //craftable:4,17

    /*public string[] item= new string[50];
    //mancare
    item[0]="Paine";
    item[1]="Apa";
    item[2]="Conserva peste";
    item[3]="Branza";
    item[4]="Burger cu branza";
    item[5]="FUC (energy drink)";
    item[6]="Cartofi";
    item[7]="Patrunjel";
    item[8]="Grau";
    item[9]="Peste";
    item[10]="Rame";
    item[11]="Mamaliga";
    item[12]="Conserva carne";
    item[13]="Carne cruda";
    item[14]="Pateu ardealu'";
    item[15]="Mancare animale";
    item[16]="Pizza";
    item[17]="Paine cu cartofi";
    item[18]="Cafea";
    //heal
    item[19]="Bandaje";
    item[20]="Plasturi";
    item[21]="Seringa anti-cariatii";
    item[22]="Sedative";
    item[23]="Paracetamol";
    item[24]="Agheazma";
    item[25]="Cafea poleita cu aur";
    item[26]="Paine sfanta";
    //de craftat
    item[27]="Chibrituri";
    item[28]="Paie/Iarba uscata";
    item[29]="Blana";
    item[30]="Conserva goala";
    //altele importante
    item[31]="Keycard rosu";
    item[32]="Keycard albastru";
    item[33]="Keycard galben";
    item[34]="Cartea jurnal de bord";
    //altele
    item[35]="Lemn"
    item[36]="Piatra"
    item[37]="Bete"
    item[38]="Funie"
    item[39]="Cruce Fier"
    item[40]="Filtru"
    item[41]="Cuie"
    item[42]="Lumanari"
    item[43]="Marker"
    item[44]="Cristelnita"
    item[45]="Unealta multifunctionala"
    item[46]="Cana"
    item[47]="Vaza"
    item[48]="Cutit"
    item[49]="Haine"
    item[50]="Carti"
    item[51]="Plasa" //(peste)
    item[52]="Ghiozdan"
    item[53]="Sapca"
    item[54]="Carma"
    //arme+gear
    item[55]="Munitie"
    item[56]="Cartuse de munitie"
    item[57]="Pistol";
    item[58]="Ak18";
    item[59]="M4";
    item[60]="Surpesor";
    item[61]="Scope";
    */


    public Dictionary<string, int> Use = new Dictionary<string, int>
    {
        {"0hunger", 50},
        {"0health", 5},
        {"0radiation", 0},
        {"1hunger", 5},
        {"1health", 5},
        {"1radiation", 0},
        {"2hunger", 40},
        {"2health", 7},
        {"2radiation", 0},
        {"3hunger", 25},
        {"3health", 1},
        {"3radiation", 0},
        {"4hunger", 75},
        {"4health", 0},
        {"4radiation", 0},
        {"5hunger", 5},
        {"5health", 25},
        {"5radiation", 5},
        {"6hunger", 30},
        {"6health", 0},
        {"6radiation", 0},
        {"7hunger", 2},
        {"7health", 0},
        {"7radiation", 0},
        {"8hunger", 5},
        {"8health", 0},
        {"8radiation", 0},
        {"9hunger", 25},
        {"9health", -5},
        {"9radiation", 0},
        {"10hunger", 5},
        {"10health", 0},
        {"10radiation", 0},
        {"11hunger", 55},
        {"11health", 0},
        {"11radiation", 0},
        {"12hunger", 45},
        {"12health", 0},
        {"12radiation", 0},
        {"13hunger", 65},
        {"13health", -15},
        {"13radiation", 0},
        {"14hunger", 35},
        {"14health", 0},
        {"14radiation", 0},
        {"15hunger", 15},
        {"15health", 0},
        {"15radiation", 0},
        {"16hunger", 45},
        {"16health", 0},
        {"16radiation", 0},
        {"17hunger", 55},
        {"17health", 0},
        {"17radiation", 0},
        {"18hunger", 5},
        {"18health", 15},
        {"18radiation", 0},
        {"19hunger", 0},
        {"19health", 55},
        {"19radiation", 0},
        {"20hunger", 0},
        {"20health", 30},
        {"20radiation", 0},
        {"21hunger", 1},
        {"21health", 0},
        {"21radiation", -40},
        {"22hunger", 1},
        {"22health", 15},
        {"22radiation", -15},
        {"23hunger", 1},
        {"23health", 30},
        {"23radiation", -15},
        {"24hunger", 5},
        {"24health", 30},
        {"24radiation", 0},
        {"25hunger", 10},
        {"25health", 50},
        {"25radiation", 0},
        {"26hunger", 45},
        {"26health", 30},
        {"26radiation", 0},
        {"27hunger", 0},
        {"27health", 0},
        {"27radiation", 0},
        {"28hunger", 0},
        {"28health", 0},
        {"28radiation", 0},
        {"29hunger", 0},
        {"29health", 0},
        {"29radiation", 0},

    };

    public Dictionary<string, int> Eat = new Dictionary<string, int>
    {
        {"0hunger", 50},
        {"0health", 5},
        {"0radiation", 0},
        {"1hunger", 5},
        {"1health", 5},
        {"1radiation", 0},
        {"2hunger", 40},
        {"2health", 7},
        {"2radiation", 0},
        {"3hunger", 25},
        {"3health", 1},
        {"3radiation", 0},
        {"4hunger", 75},
        {"4health", 0},
        {"4radiation", 0},
        {"5hunger", 5},
        {"5health", 25},
        {"5radiation", 5},
        {"6hunger", 30},
        {"6health", 0},
        {"6radiation", 0},
        {"7hunger", 2},
        {"7health", 0},
        {"7radiation", 0},
        {"8hunger", 5},
        {"8health", 0},
        {"8radiation", 0},
        {"9hunger", 25},
        {"9health", -5},
        {"9radiation", 0},
        {"10hunger", 5},
        {"10health", 0},
        {"10radiation", 0},
        {"11hunger", 55},
        {"11health", 0},
        {"11radiation", 0},
        {"12hunger", 45},
        {"12health", 0},
        {"12radiation", 0},
        {"13hunger", 65},
        {"13health", -15},
        {"13radiation", 0},
        {"14hunger", 35},
        {"14health", 0},
        {"14radiation", 0},
        {"15hunger", 15},
        {"15health", 0},
        {"15radiation", 0},
        {"16hunger", 45},
        {"16health", 0},
        {"16radiation", 0},
        {"17hunger", 55},
        {"17health", 0},
        {"17radiation", 0},
        {"18hunger", 5},
        {"18health", 15},
        {"18radiation", 0},
        {"19hunger", 1},
        {"19health", -20},
        {"19radiation", 0},
        {"20hunger", 1},
        {"20health", -20},
        {"20radiation", 0},
        {"21hunger", 1},
        {"21health", -20},
        {"21radiation", 0},
        {"22hunger", 1},
        {"22health", 15},
        {"22radiation", -15},
        {"23hunger", 1},
        {"23health", 30},
        {"23radiation", -15},
        {"24hunger", 5},
        {"24health", 30},
        {"24radiation", 0},
        {"25hunger", 10},
        {"25health", 50},
        {"25radiation", 0},
        {"26hunger", 45},
        {"26health", 30},
        {"26radiation", 0},
        {"27hunger", 1},
        {"27health", -20},
        {"27radiation", 0},
        {"28hunger", 7},
        {"28health", -5},
        {"28radiation", 0},
        {"29hunger", 1},
        {"29health", -5},
        {"29radiation", 0},
        {"30hunger", 1},
        {"30health", -90},
        {"30radiation", 0},
        {"31hunger", 1},
        {"31health", -90},
        {"31radiation", 0},
        {"32hunger", 1},
        {"32health", -90},
        {"32radiation", 0},
        {"33hunger", 1},
        {"33health", -90},
        {"33radiation", 0},
        {"34hunger", 1},
        {"34health", -90},
        {"34radiation", 0},
        {"35hunger", 1},
        {"35health", -90},
        {"35radiation", 0},
        {"36hunger", 1},
        {"36health", -90},
        {"36radiation", 0},
    };

}
