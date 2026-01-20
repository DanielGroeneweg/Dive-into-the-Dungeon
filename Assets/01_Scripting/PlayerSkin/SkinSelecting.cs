using UnityEngine;
using System.Collections.Generic;
using System;

public class SkinSelecting : MonoBehaviour
{
    [Serializable]
    private class FaceParts
    {
        [Header("Part Lists")]
        public List<SkinnedMeshRenderer> noses;
        public List<SkinnedMeshRenderer> hairs;
        public List<SkinnedMeshRenderer> eyes;
        public List<SkinnedMeshRenderer> eyebrows;
        public List<SkinnedMeshRenderer> facehairs;
        public List<SkinnedMeshRenderer> ears;

        [Header("References")]
        public Transform nose;
        public Transform hair;
        public Transform eye;
        public Transform eyebrow;
        public Transform facehair;
        public Transform ear;

        [HideInInspector] public int noseIndex;
        [HideInInspector] public int hairIndex;
        [HideInInspector] public int eyeIndex;
        [HideInInspector] public int eyebrowIndex;
        [HideInInspector] public int facehairIndex;
        [HideInInspector] public int earIndex;
    }

    [SerializeField] private FaceParts faceParts = new FaceParts();

    // Generic switch method
    private void SwitchPart(
        List<SkinnedMeshRenderer> parts,
        ref int index,
        ref Transform currentPart,
        int direction
    )
    {
        if (parts == null || parts.Count == 0) return;

        index += direction;

        if (index >= parts.Count) index = 0;
        else if (index < 0) index = parts.Count - 1;

        if (currentPart != null)
            currentPart.gameObject.SetActive(false);

        currentPart = parts[index].transform;
        currentPart.gameObject.SetActive(true);
    }

    // NOSE
    public void NextNose() =>
        SwitchPart(faceParts.noses, ref faceParts.noseIndex, ref faceParts.nose, +1);

    public void PrevNose() =>
        SwitchPart(faceParts.noses, ref faceParts.noseIndex, ref faceParts.nose, -1);

    // HAIR
    public void NextHair() =>
        SwitchPart(faceParts.hairs, ref faceParts.hairIndex, ref faceParts.hair, +1);

    public void PrevHair() =>
        SwitchPart(faceParts.hairs, ref faceParts.hairIndex, ref faceParts.hair, -1);

    // EARS
    public void NextEar() =>
        SwitchPart(faceParts.ears, ref faceParts.earIndex, ref faceParts.ear, +1);

    public void PrevEar() =>
        SwitchPart(faceParts.ears, ref faceParts.earIndex, ref faceParts.ear, -1);

    // FACE HAIR
    public void NextFacehair() =>
        SwitchPart(faceParts.facehairs, ref faceParts.facehairIndex, ref faceParts.facehair, +1);

    public void PrevFacehair() =>
        SwitchPart(faceParts.facehairs, ref faceParts.facehairIndex, ref faceParts.facehair, -1);

    // EYES
    public void NextEyes() =>
        SwitchPart(faceParts.eyes, ref faceParts.eyeIndex, ref faceParts.eye, +1);

    public void PrevEyes() =>
        SwitchPart(faceParts.eyes, ref faceParts.eyeIndex, ref faceParts.eye, -1);

    // EYEBROWS
    public void NextEyebrows() =>
        SwitchPart(faceParts.eyebrows, ref faceParts.eyebrowIndex, ref faceParts.eyebrow, +1);

    public void PrevEyebrows() =>
        SwitchPart(faceParts.eyebrows, ref faceParts.eyebrowIndex, ref faceParts.eyebrow, -1);
}