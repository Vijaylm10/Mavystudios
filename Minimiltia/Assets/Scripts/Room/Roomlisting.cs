using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Roomlisting : MonoBehaviourPunCallbacks
{

    public Transform content;
    public Roominfo roomlisting;
    public List<Roominfo> rooms = new List<Roominfo>();
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {

            if (info.RemovedFromList)
            {
                int index = rooms.FindIndex(x => x.RoomInfos.Name == info.Name);
                if (index != -1)
                {
                    Destroy(rooms[index].gameObject);
                    rooms.RemoveAt(index);
                    rooms.Clear();
                }
            }
            else
            {
                int index = rooms.FindIndex(x => x.RoomInfos.Name == info.Name);
                if (index == -1)
                {
                    Roominfo list = Instantiate(roomlisting, content);

                    if (list != null)
                    {
                        list.SetRoomInfo(info);
                        rooms.Add(list);
                    }
                }
            }

        }
    }

    public override void OnJoinedRoom()
    {
        content.Destroychildren();
        rooms.Clear();
    }
}
