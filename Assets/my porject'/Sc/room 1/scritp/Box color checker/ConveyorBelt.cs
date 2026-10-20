using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [Header("Conveyor Settings")]
    [Tooltip("ความเร็วในการเคลื่อนที่ของสายพาน")]
    public float speed = 1.5f;

    [Tooltip("ทิศทางการเคลื่อนที่ของสายพาน (อ้างอิงตาม Axis ของ Unity)")]
    public Vector3 direction = Vector3.forward;

    private void OnCollisionStay(Collision collision)
    {
        // ดึง Rigidbody ของวัตถุที่ชนอยู่ (เช่น ตัวละคร/ของบนสายพาน)
        Rigidbody rb = collision.rigidbody;

        if (rb != null && !rb.isKinematic)
        {
            // คำนวณทิศทางการเลื่อนตาม transform ของสายพาน
            Vector3 moveDir = transform.TransformDirection(direction.normalized) * speed;

            // ใช้ velocity แทน MovePosition เพื่อไม่ให้ physics solver ตีกัน
            // (MovePosition จะ teleport ตำแหน่งแบบไม่ sweep collision
            // ทำให้เกิดการซ้อนทับกับ collider แล้วโดนดันกลับ = เด้งอยู่ที่เดิม)
            Vector3 currentVel = rb.linearVelocity; // ใน Unity เก่าใช้ rb.velocity

            // คงความเร็วแนวตั้ง (แรงโน้มถ่วง/กระโดด) ไว้ตามเดิม
            // แล้วบวกความเร็วแนวราบของสายพานเข้าไป
            Vector3 verticalVel = Vector3.Project(currentVel, Vector3.up);
            rb.linearVelocity = moveDir + verticalVel;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // เมื่อวัตถุหลุดออกจากสายพานแล้ว ไม่ต้องทำอะไรเพิ่ม
        // (ถ้าอยาก reset velocity ตอนหลุด ค่อยเพิ่ม logic ตรงนี้)
    }
}