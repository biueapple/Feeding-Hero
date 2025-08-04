using UnityEngine;

[CreateAssetMenu(menuName = "Game/Visitor")]
public class VisitorData : ScriptableObject
{
    //�̸�
    public string visitorName;
    //�̹���
    public Sprite portrait;

    //���� ����
    public TradeType tradeType;
    //������
    public ItemData targetItem;
    //����
    public int price;
    //���� (�󸶳� ���ݿ� �������� ����)
    //�������� ��� (�÷��̾ �춧�� �̸�ŭ �ε� ���� �ȴ�� �̸�ŭ ��ε� ����)
    [Range(0, 10)]
    public int successRange;
    //�׳� ����
    [Range(0, 10)]
    public int failRange;


    //��� ��ȸ�� �ִ���
    public int chance;
    //��� ���� (���, �ŷ� ����, ����, �Ҹ���)
    public string meeting;
    public string success;
    public string fail;
    public string[] unsatisfactory;
}
